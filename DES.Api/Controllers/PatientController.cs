using Data;
using DataTablesFilters;
using DataTablesHelper;
using Dto;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace DES.Api.Controllers
{
    [Route("api/[controller]")]    //api/PatientUnitOfWork/GetAll
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            var res = _unitOfWork.Patients.GetAll();
            return Ok(res);
        }
        [HttpPost("Add")]
        public IActionResult Add(PatientDto dto)
        {
            //get curren user authotnicated
            //  var _userId = int.Parse(User.Identity.Name);
            Patient patient = new Patient()
            {
                PatientName = dto.PatientName,
                BirtDate = dto.BirtDate,
                AreaId = (int)dto.AreaId,
                NID = dto.NID,
                CreatedBy = "Ahmed",
                CreatedOn = DateTime.Now
            };
            _unitOfWork.Patients.Add(patient);
            _unitOfWork.Save();
            return Ok(patient.Id);
        }


        #region Patient
        [HttpPost("GetPatients")]
        public IActionResult GetPatients([FromBody] DataTableApi<PubDtFilter> model)
        {
            var pageSize = model.DataTableInfo.PageSize;
            var skip = model.DataTableInfo.skip;
            var searchValue = model.DataTableInfo.SearchValue;
            var sortColumn = model.DataTableInfo.SortColumn;
            var sortColumnDirection = model.DataTableInfo.SortColumnDirection;

             var criteria = BuildCriteria<PatientsV>(model.FilterC, searchValue);


            var _data = _unitOfWork.PatientsV.FindWithFilters(
               criteria: criteria,
               sortColumn: sortColumn,
               sortColumnDirection: sortColumnDirection,
               skip: skip,
               take: pageSize);
            var recordsTotal = _unitOfWork.PatientsV.FindWithFilters(criteria: criteria).Count();

            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = _data };

            return Ok(jsonData);
        }
        private Expression<Func<T, bool>> BuildCriteria<T>(PubDtFilter filter, string searchValue)
        {
            var parameterExp = Expression.Parameter(typeof(T));
            Expression conditionExp = Expression.Constant(true);

            try
            {
                var filterProperty = typeof(PubDtFilter).GetProperty("PatientName");
                var propValue = filterProperty.GetValue(filter);
                var entityTypeProperties = typeof(T).GetProperties();
                if (propValue != null && (int)propValue != 0)
                {
                    // Assuming T has a property named "Id" to match with PubDtFilter's Id
                    var entityProperty = typeof(T).GetProperty("PatientName");

                    if (entityProperty != null)
                    {
                        Expression propExp = Expression.Property(parameterExp, entityProperty.Name);
                        conditionExp = Expression.AndAlso(conditionExp, Expression.Equal(propExp, Expression.Constant(propValue)));
                    }
                }

                // If SearchValue is not empty, add criteria to check if any column contains the SearchValue
                if (!string.IsNullOrEmpty(searchValue))
                {
                    Expression searchExp = Expression.Constant(false);
                    foreach (var entityProp in entityTypeProperties)
                    {
                        var entityPropExp = Expression.Property(parameterExp, entityProp.Name);
                        if (entityProp.PropertyType == typeof(string))
                        {
                            var containsExp = Expression.Call(entityPropExp, "Contains", null, Expression.Constant(searchValue));
                            searchExp = Expression.OrElse(searchExp, containsExp);
                        }
                        else if (Nullable.GetUnderlyingType(entityProp.PropertyType) == typeof(int) && int.TryParse(searchValue, out int intValue))
                        {

                            var hasValueExp = Expression.Property(entityPropExp, "HasValue");
                            var valueExp = Expression.Property(entityPropExp, "Value");
                            var equalsExp = Expression.Equal(valueExp, Expression.Constant(intValue));
                            var nullableIntExp = Expression.AndAlso(hasValueExp, equalsExp);
                            searchExp = Expression.OrElse(searchExp, nullableIntExp);
                        }
                    }
                    conditionExp = Expression.AndAlso(conditionExp, searchExp);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error building criteria", ex);
            }

            var lambdaExp = Expression.Lambda<Func<T, bool>>(conditionExp, parameterExp);
            return lambdaExp;

            #endregion

        }
    }
}
