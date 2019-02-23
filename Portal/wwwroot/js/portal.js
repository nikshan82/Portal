var id = 1;
var table;
var classTable;
$(function () {
    debugger;
    $('#dialogLanguage').dialog({
        dialogClass: "noOverlayDialog",
        autoOpen: false,
        draggable: false,
        resizable: false,
        modal: true
    });
    classTable = $('#tblClassData').dataTable({
        "serverSide": true,
        "order": [[1, "asc"]],
        "bPaginate": false,
        "searching": false, "paging": false, "info": false,
        "ajax": {
            "url": "/Home/GetDataAsync",
            "dataSrc": "data"
        },
        "createdRow": function (row, data, index) {
        },
        "columns": [
            { "data": "id", "visible": false },
            { "data": "name", "orderable": true, "visible": true },
            { "data": "location", "orderable": true, "visible": true },
            { "data": "teacher", "orderable": true, "visible": true },
            {
                mRender: function (data, type, row) {
                    return '<button class="editClass" data-id="' + row.id + '">Edit</button>'
                }, "orderable": false
            },
            {
                mRender: function (data, type, row) {
                    return '<button class="deleteClass" data-id="' + row.id + '">Delete</button>'
                }, "orderable": false
            }
        ]
    });
    table = $('#tblStudentData').DataTable({
        "serverSide": true,
        "order": [[1, "asc"]],
        "bPaginate": false,
        "searching": false, "paging": false, "info": false,
        "ajax": {
            "url": "/Home/GetStudentDataAsync",
            "data": function (d) {
                d.Id = GetId();
            },
            "dataSrc": "data"
        },
        "createdRow": function (row, data, index) {
        },
        "columns": [
            { "data": "id", "orderable": true, "visible": false },
            {
                mRender: function (data, type, row) {
                    if (row.gpa > 3) {
                        return '<span>' + row.fullName + '</span><span style="color:yellow">☆</span>'
                    } else {
                        return '<span>' + row.fullName + '</span>'
                    }
                }
            },
            { "data": "age", "orderable": true, "visible": true },
            { "data": "gpa", "orderable": true, "visible": true },
            {
                mRender: function (data, type, row) {
                    return '<button class="editStudent" data-id="' + row.id + '">Edit</button>'
                }, "orderable": false
            },
            {
                mRender: function (data, type, row) {
                    return '<button class="editStudent" data-id="' + row.id + '">Delete</button>'
                }, "orderable": false
            }
        ]
    });

    $("#addClass").click(function (e) {
        event.stopPropagation();
        $('[aria-describedby=dialogLanguage] .ui-dialog-title').text('Add Class');
        $('#dialogLanguage').data('id', 0).dialog('open');
    });

}).on('click', '.editClass', function (event) {
    event.stopPropagation();
    var data = $('#tblClassData').DataTable().row($(this).closest('tr')).data();
    $('[aria-describedby=dialogLanguage] .ui-dialog-title').text('Edit Class');
    $('#dialogLanguage').data('id', data.id).dialog('open');
    $("#ClassName").val(data.name);
    $("#LocationName").val(data.location);
    $("#TeacherName").val(data.teacher);
    debugger;
}).on('click', '#tblClassData tbody tr', function (event) {
    var data = $('#tblClassData').DataTable().row(this).data();
    $("#labelClass").text(data.name);
    $("#hiddenId").val(data.id);
    debugger;
    id = 2;
    table.ajax.reload();
}).on('click', '#saveClassButton', function (event) {
    event.stopPropagation();
    var data = GetClassFormDetails();
    $.ajax({
        type: "POST",
        url: "/Home/UpdateClassRecord",
        data: { Id: data.id, Name: data.Name, Location: data.Location, Teacher: data.Teacher },
        dataType: "JSON",
        success: function (dto) {
            debugger;
            $('#dialogLanguage').dialog('close');
            $('#tblClassData').DataTable().ajax.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}).on('click', '.deleteClass', function (event) {
    event.stopPropagation();
    var data = $('#tblClassData').DataTable().row($(this).closest('tr')).data();
    debugger;
    $.ajax({
        type: "POST",
        url: "/Home/DeleteClassRecord",
        data: { Id: data.id },
        dataType: "JSON",
        success: function (dto) {
            debugger;
            $('#tblClassData').DataTable().ajax.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
});

function GetId() {
    debugger;
    return $("#hiddenId").val();
}

function RefreshTable(tableId, urlData) {
    ajax.reload();
}

function GetClassFormDetails() {
    return { Name: $("#ClassName").val(), Location: $("#LocationName").val(), Teacher: $("#TeacherName").val(), Id: $("#dialogLanguage").data('id') };
}

function GetStudentFormDetails() {
    return { Name: $("#ClassName").val(), Location: $("#LocationName").val(), Teacher: $("#TeacherName").val(), Id: $("#dialogLanguage").data('id') };
}

///testst