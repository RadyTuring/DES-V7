const toggler = document.querySelector(".btn");
toggler.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("collapsed");
});
$(document).ready(function () {
    $('#search-select').select2();
    $('#multi-search-select').select2();
});

$(document).ready(function () {
    $('#collapseSection').on('show.bs.collapse', function () {
        $('#toggleIcon').removeClass('bi-funnel').addClass('bi-funnel-fill');
    }).on('hide.bs.collapse', function () {
        $('#toggleIcon').removeClass('bi-funnel-fill').addClass('bi-funnel');
    });
});
new DataTable('#_datatable', {
    layout: {
        topStart: {
            buttons: ['pageLength', 'pdf', 'excel', 'print', 'copy', 'colvis', 'colvisRestore'],
        },

    }
});
