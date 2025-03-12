using DataTablesFilters;
using DataTablesHelper;
using Dto;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Services;
using System.Diagnostics.Metrics;
using System.Text;

namespace DES.Client.Controllers
{
    public class PatientController : Controller
    {
        private ICallApi _api;
        public PatientController(ICallApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var rolePages = JsonConvert.DeserializeObject<IEnumerable<RolePage>>(ViewData["rolepages"].ToString());
           
            RolePage rolePage = rolePages.Where(m => m.DesPageId == 5).FirstOrDefault();    //5 is id of page patent in db
            ViewBag.isadd = rolePage.IsAdd;
            ViewBag.isdelete = rolePage.IsDelete;
            ViewBag.isupdate = rolePage.IsUpdate;

            var res = _api.Get<Patient>("Patient\\GetAll");
            return View(res);
        }
        public IActionResult Add()
        {
            

            var govs = _api.Get<Area>("Area\\GetGovs");
            ViewBag.govs = new SelectList( govs,"Level","LName");
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(PatientDto model)
        {
            if (ModelState.IsValid)
            {
                _api.Create("Patient/Add", model);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetDistricts(string id)
        {
            var districts = _api.Get<Area>($"Area/GetDistsByGovID?id={id}");
            return Json(districts);
        }
        [HttpGet]
        public IActionResult GetSourcesByDistId(string id)
        {
            var reportSources = _api.Get<Area>($"Area/GetSourcesByDistID?id={id}");
            return Json(reportSources);
        }

        public IActionResult FilterPatients()
        {
            ViewBag.filters = GenerateJavaScriptForFilters();
            ViewBag.columns = GenerateJavaScriptForColumns();
            ViewBag.actions = GenerateJavaScriptForActions();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FilterPatients(PubDtFilter model)
        {
            ViewBag.filters = GenerateJavaScriptForFilters();
            ViewBag.columns = GenerateJavaScriptForColumns();
            ViewBag.actions = GenerateJavaScriptForActions();

            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);
            var searchValue = Request.Form["search[value]"];
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            DataTableInfo dtInfo = new DataTableInfo()
            {
                PageSize = pageSize,
                skip = skip,
                SearchValue = searchValue,
                SortColumn = sortColumn,
                SortColumnDirection = sortColumnDirection

            };
            DataTableApi<PubDtFilter> dtApi = new DataTableApi<PubDtFilter>()
            {
                DataTableInfo = dtInfo,
                FilterC = model
            };


            try
            {
                var result = await _api.PostDt<DataTableApi<PubDtFilter>, DataTableData<PatientsV>>("Patient/GetPatients", dtApi);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return BadRequest();

        }


        #region DataTable

        private string GenerateJavaScriptForFilters()
        {
            var properties = typeof(PubDtFilter).GetProperties();
            var stringBuilder = new StringBuilder("\"data\": function (d) {");
            foreach (var prop in properties)
            {
                stringBuilder.AppendLine($"d.{prop.Name}=$('#{prop.Name}').val();");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }


        private string GenerateJavaScriptForColumns()
        {
            var properties = typeof(PatientsV).GetProperties();
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("columns: [");

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(DateTime?) || prop.PropertyType == typeof(DateTime))
                {
                    stringBuilder.AppendLine($"    {{ \"data\": \"{ToCamelCase(prop.Name)}\", \"name\": \"{prop.Name}\",");
                    stringBuilder.AppendLine("      \"render\": function (data, type, row) {");
                    stringBuilder.AppendLine("          return data ? '<span>' + data.split('T')[0] + '</span>' : ''; } },");
                }
                else
                {
                    stringBuilder.AppendLine($"    {{ \"data\": \"{ToCamelCase(prop.Name)}\", \"name\": \"{prop.Name}\" }},");
                }
            }



            // New details and profile buttons column
            stringBuilder.AppendLine("    { \"data\": null, \"orderable\": false,");
            stringBuilder.AppendLine("      \"render\": function (data, type, row) {");
            stringBuilder.AppendLine("          return `");
            stringBuilder.AppendLine("              <a href=\"/Account/Edit/${row.userId}\" ><i class='fas fa-edit'></i></a>");
            stringBuilder.AppendLine("              <a href=\"/Manager/GetLog/${row.userId}\" ><i class='fas fa-history'></i></a>");
            stringBuilder.AppendLine("              <a href=\"/Account/ResetPassword/${row.userId}\" ><i class='fas fa-unlock-alt'></i></a>`; }");
            stringBuilder.AppendLine("    },");


            // New select action column
            stringBuilder.AppendLine("    { \"data\": null, \"orderable\": false,");
            stringBuilder.AppendLine("      \"render\": function (data, type, row) {");
            stringBuilder.AppendLine("          return `");
            stringBuilder.AppendLine("              <select class=\"action-select\" data-id=\"${row.id}\">");
            stringBuilder.AppendLine("                  <option value=\"\">Select action</option>");
            stringBuilder.AppendLine("                  <option value=\"block\">${row.isActiveUser==1 ? 'Block' : 'Unblock'}</option>");
            stringBuilder.AppendLine("                  <option value=\"update\">Update</option>");
            stringBuilder.AppendLine("                  <option value=\"details\">Go to Details</option>");
            stringBuilder.AppendLine("              </select>`; }");
            stringBuilder.AppendLine("    }");



            stringBuilder.AppendLine("]");

            return stringBuilder.ToString();
        }

        private string GenerateJavaScriptForActions()
        {
            var stringBuilder = new StringBuilder();
            //execute the code by enter
            stringBuilder.AppendLine("    $(document).keydown(function (event) {");
            stringBuilder.AppendLine("        if (event.key === 'Enter') {");
            stringBuilder.AppendLine("            $('#btnSearch').click();");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    });");

            // Event listener for btnSearch click
            stringBuilder.AppendLine("    $('#btnSearch').click(function () {");
            stringBuilder.AppendLine("        table.ajax.reload();");
            stringBuilder.AppendLine("    });");

            // Event listener for .action-select change within #Customers
            stringBuilder.AppendLine("    $('#users').on('change', '.action-select', function () {");
            stringBuilder.AppendLine("        var id = $(this).data('id');");
            stringBuilder.AppendLine("        var action = $(this).val();");
            stringBuilder.AppendLine("        if (action) {");
            stringBuilder.AppendLine("            if (action === 'block') {");
            stringBuilder.AppendLine("                BlockUser(id);");
            stringBuilder.AppendLine("            } else if (action === 'update') {");
            stringBuilder.AppendLine("                UpdateCustomer(id);");
            stringBuilder.AppendLine("            } else if (action === 'details') {");
            stringBuilder.AppendLine("                GoToDetails(id);");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            // Reset the select to the default option");
            stringBuilder.AppendLine("            $(this).val('');");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    });");

            // End JavaScript initialization
            stringBuilder.AppendLine("});");

            // Function to handle customer deletion
            stringBuilder.AppendLine("function DeleteCustomer(id) {");
            stringBuilder.AppendLine("    if (confirm(\"Are you sure you want to Change Status of This user?\")) {");
            stringBuilder.AppendLine("        $.ajax({");
            stringBuilder.AppendLine("            url: '/Manager/BlockUser',");
            stringBuilder.AppendLine("            type: 'POST',");
            stringBuilder.AppendLine("            data: { id: id },");
            stringBuilder.AppendLine("            success: function (response) {");
            stringBuilder.AppendLine("                if (response.success) {");
            stringBuilder.AppendLine("                    alert('Customer deleted successfully.');");
            stringBuilder.AppendLine("                    $('#users').DataTable().ajax.reload();");
            stringBuilder.AppendLine("                } else {");
            stringBuilder.AppendLine("                    alert('Error Changing Status of This user.');");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("            },");
            stringBuilder.AppendLine("            error: function () {");
            stringBuilder.AppendLine("                alert('Error deleting customer.');");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        });");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            // Function to handle customer update
            stringBuilder.AppendLine("function UpdateCustomer(id) {");
            stringBuilder.AppendLine("    // Redirect to the update page or open a modal for updating");
            stringBuilder.AppendLine("    window.location.href = '/Customer/Update/' + id;");
            stringBuilder.AppendLine("}");

            // Function to handle customer details
            stringBuilder.AppendLine("function GoToDetails(id) {");
            stringBuilder.AppendLine("    // Redirect to the details page");
            stringBuilder.AppendLine("    window.location.href = '/Customer/Details/' + id;");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
        }


        private string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return str;
            return char.ToLower(str[0]) + str.Substring(1);
        }
        #endregion

    }
}
