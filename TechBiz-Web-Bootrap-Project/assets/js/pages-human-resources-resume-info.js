/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function() {



    //============================================================================
    // ZONE : RUN INIT
    // create date   : 20240712 
    // create by     : Nat
    // detail        :
    //============================================================================

    var Page = {
        mainTableControl: {}
    }

    const token = localStorage.getItem("token");
    const userId = localStorage.getItem("user_id");
    const username = localStorage.getItem("user_name");
    if (!token || !userId||!username) {
        window.location.href= 'login.html';
    }



    console.log("userId"+userId);
    ShowMainListDataTable();

    
   
    //============================================================================
    // ZONE : SUBMIT
    // create date   : 20240712 
    // create by     : Nat
    // detail        :
    //============================================================================
    $('#AddFormId').submit(async function(event) {
        event.preventDefault();

        var formData = $(this).serializeObject (); 
        console.log(formData);
        AddFromSubmit(formData);
        
    });

    $('#UploadFormId').submit(async function(event) {
        event.preventDefault();

        var formData = new FormData(this); 
        console.log(formData);
        
        ShowUploadListDataTable(formData)
        
    });
    
    //============================================================================
    // ZONE : EVENT CLICK
    // create date   : 20240712 
    // create by     : Nat
    // detail        :
    //============================================================================

    $(document).on('click', '#logoutId', function() {
        console.log("Log out");
        window.location.href = 'login.html'; 
    });

    $(document).on('click', '#AddNewId', function() {
        $('#ToppicActionId').text('Add Job');
        ClearAddNew();
        $('.SlideTabSearch').hide();
        $('#AreaAddId').show();
    });

    $(document).on('click', '#CloseId', function() {
        $('.SlideTabSearch').hide();
        $('#AreaSearchId').show();
    });



    $(document).on('click', '.EditButton', function() {
        console.log("Edit");
        $('#ToppicActionId').text('Edit Job');
        $('#saveId').prop('disabled', false);
        var id = $(this).attr('data-id');
        RenderAddNewForm(id);
        

        $('.SlideTabSearch').hide();
        $('#AreaAddId').show();
    });
    
    $(document).on('click', '.ViewButton',async function() {
        console.log("View");
        $('#ToppicActionId').text('View Job');
        $('#saveId').prop('disabled', true);
        // alert("Test");
        var id = $(this).attr('data-id');
        RenderAddNewForm(id);
        $('.SlideTabSearch').hide();
        $('#AreaAddId').show();
       
    });
    
    $(document).on('click', '.DeleteButton', function() {
        MessageBox.ConfirmDeletedMessage("", () => {
          var id = $(this).attr('data-id');
          return $.ajax({
            type: 'DELETE',
            headers: {"Authorization": "Bearer " + token},
            contentType: 'application/json; charset=utf-8',
            url: config.apiUrl.base + '/api/hr/job/Delete/' + id
          }).then((response) => {
            return true;
          }).catch((error) => {
            console.error("Error deleting item", error);
            return false;
          });
        }).then((success) => {
          if (success) {
            Page.mainTableControl.ajax.reload();
          }
        });
      });
          //>>>> Event Click active assy part control
    $(document).on('click', '.ActiveButton', function () {
        var button = $(this);
        var id = $(this).attr('data-id');
        var loginId = 10;

        $('body').addClass('loading');
        if (button.hasClass('btn-success')) {
            button.removeClass('btn-success').addClass('btn-secondary');
            $.ajax({
                url: config.apiUrl.base + '/api/hr/job/activateCondition' + id + '&user_id=' + loginId + '&is_active=false',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                data: {

                },
                headers: {"Authorization": "Bearer " + token},
                success: function(response) {
                    console.log(response);
                    
                    if(response.status != true){
                        alert("Error Code : "+response.code +" Error Description " +response.description);
                        MessageBox.ErrorMessage(response.code,response.description);
                    }
                   
                },
                error: function(xhr, status, error) {
                    
                    console.error(xhr.responseText);
                    if (xhr.status === 401) {
                        alert("Session expired. Redirecting to login page.");
                        window.location.href = 'login.html'; 
                        localStorage.removeItem("token");
                    }
                    alert('Error in form submission');
                }
            });

        } else {
            button.removeClass('btn-secondary').addClass('btn-success');
            $.ajax({
                url: config.apiUrl.base + '/api/hr/job/activateCondition' + id + '&user_id=' + loginId + '&is_active=true',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                data: {

                },
                headers: {"Authorization": "Bearer " + token},
                success: function(response) {
                    console.log(response);
                    
                    if(response.status != true){
                        alert("Error Code : "+response.code +" Error Description " +response.description);
                        MessageBox.ErrorMessage(response.code,response.description);
                    }
                   
                },
                error: function(xhr, status, error) {
                    
                    console.error(xhr.responseText);
                    if (xhr.status === 401) {
                        alert("Session expired. Redirecting to login page.");
                        window.location.href = 'login.html'; 
                        localStorage.removeItem("token");
                    }
                    alert('Error in form submission');
                }
            });
        }
    });


    $(document).on('click', '#UploadCloseId', function() {
        $('.SlideTabUpload').hide();
        $('#AreaUploadId').show();
    });

    //============================================================================
    // ZONE : CUSTOM FUNCTION
    // create date   : 20240712 
    // create by     : Nat
    // detail        :
    //============================================================================

    function ShowMainListDataTable()
    {
          Page.mainTableControl = $('#tableShowListId').DataTable({
            layout: {
                top1Start: {
                    buttons: [
                        'copy',
                        { 
                            extend:'print',
                            exportOptions: {
                                columns: [1,2,3,4],
                            }
                        },
                        {
                            extend: 'spacer',
                            style: 'bar',
                            text: 'Export files:'
                        },
                        'csv',
                        {
                            extend: 'excelHtml5',
                            autoFilter: true,
                            sheetName: 'Exported data',
                            exportOptions: {
                                columns: [1,2,3,4,5],
                            }
                        },
                        {
                            extend: 'pdfHtml5',
                            download: 'open',
                            exportOptions: {
                                columns: [1,2,3,4,5],
                            }
                        },
                        'colvis'
                    ]
                }
            },
            responsive: true,
            searching: true,
            paging: true,
            serverSide: true,
            ajax: (data, callback) => {
                $.ajax({
                    
                    url: config.apiUrl.base+'/api/hr/job/GetPaginate',
                    type: 'GET',
                    contentType: 'application/json; charset=utf-8',
                    headers: {"Authorization": "Bearer " + token},
                    beforeSend: () => {
                        $('body').addClass('loading');
                    },
                    data: {
                        page: Math.ceil(data.start / data.length) + 1,
                        limit: data.length,
                        sortBy: getTableSortBy(data.order[0].column),
                        sortType: data.order[0].dir,
                        searchValue : data.search.value
                    },
                    success: (data) => {
                        $('body').removeClass('loading');
                        callback({
                            data: data.data.data || data,
                            recordsTotal: data.data.total || data.length || 0,
                            recordsFiltered: data.data.total || data.length || 0,
                        });

                    },
                    error: () => {
                        $('body').removeClass('loading');
                    }
                });
            }, // ajax
            lengthMenu: [[10, 25, 50, 75, 100], [10, 25, 50, 75, 100]],
            aaSorting: [[1, 'asc']],
            language: {
               /* paginate: {
                    next: '<span aria-hidden="true">></span>',
                    previous: '<span aria-hidden="true"><</span>',
                }*/
            },       
           columnDefs: [
                {
                    targets: 0,
                    orderable: false,
                    className: 'dt-body-center',
                    render: function (data, type, row) {
                      //  return '<button type="button" class="btn btn-success btn-sm" data-id="${row.id}"><i class="bx bx-power-off me-1"></i></button>';
                        return '<button type="button" class="btn ' + (row.is_active ? 'btn-success' : 'btn-secondary') + ' btn-sm  ActiveButton" data-id="${row.id}"><i class="bx bx-power-off me-1"></i></button>';
                    }
                },
                { targets: 1, data: 'hr_job_title', ordering: true },
                { targets: 2, data: 'hr_job_types', ordering: true },
                { targets: 3, data: 'hr_job_start_date', ordering: true },
                { targets: 4, data: 'hr_job_expire_date', ordering: true },
                { targets: 5, data: 'hr_job_status', ordering: true },
                {
                    targets: 6,
                    data: null,
                    orderable: false,
                    className: 'dt-head-center dt-body-center',
                    render: function (data, type, row) {
                        return `<div class="row"><button type="button" class="btn btn btn-info btn-sm ViewButton col-4" data-id="${row.hr_job_id}" ><i class="bx bx-list-ul me-1"></i></button> ` +
                            ` <button type="button" class="btn btn-primary btn-sm EditButton col-4" data-id="${row.hr_job_id}"><i class="bx bx-edit me-1"></i></button> `+
                            ` <button type="button" class="btn btn-danger btn-sm DeleteButton col-4" data-id="${row.hr_job_id}"><i class="bx bx-trash me-1"></i></button> </div>`;
                    }
                }
            ]
          });

          
    } 

    function ShowUploadListDataTable(formData)
    {

        var formData = new FormData(this);

        $.ajax({
            url: config.apiUrl.base+'/api/hr/job/ImportDataExcelFile',
            headers: {"Authorization": "Bearer " + token},
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function(response) {
                alert('File uploaded successfully.');
            },
            error: function(jqXHR, textStatus, errorThrown) {
                alert('Error uploading file: ' + textStatus);
            }
        });

          
    } 
    
    function getTableSortBy(column){
        switch (column) {
            case 1:
                return 'hr_job_title';
            case 2:
                return 'hr_job_types';
            case 3:
                return 'hr_job_start_date';
            case 4:
                return 'hr_job_expire_date';
            case 5:
                return 'hr_job_status';
            default:
                return null;
        }
    }

    async function  AddFromSubmit(formData)
    {
        var type;
        var url;
               
        //check insert or update
        if (formData.hr_job_id == '0') {
            //insert 
            formData.create_by = username;
            formData.dept_id = 0;                       
           
            var type = 'POST';
            var url =config.apiUrl.base+'/api/hr/job/addnew';
        }else{
            //update
            formData.update_by = username;
            formData.dept_id = 0;  

            type = 'PUT';
            url = config.apiUrl.base+'/api/hr/job/update';
        }
        
        // เพิ่มข้อมูลสถานะของ checkbox
        var isChecked = $('#hr_job_status').is(':checked');
        formData.hr_job_status = isChecked ? 'ACTIVE' : 'INACTIVE';

        await $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(formData),
            headers: {"Authorization": "Bearer " + token},
            success: function(response) {
                console.log(response);
                
                if(response.status == true){
                    $('#AddFormId')[0].reset();
                    MessageBox.SuccessMessage(response.code,response.description);

                }else{
                    alert("Error Code : "+response.code +" Error Description " +response.description);
                    MessageBox.ErrorMessage(response.code,response.description);
                }
               
            },
            error: function(xhr, status, error) {
                
                console.error(xhr.responseText);
                if (xhr.status === 401) {
                    alert("Session expired. Redirecting to login page.");
                    window.location.href = 'login.html'; 
                    localStorage.removeItem("token");
                }
                alert('Error in form submission');
            }
        });
        
        $('.SlideTabSearch').hide();
        $('#AreaSearchId').show();

        Page.mainTableControl.ajax.reload();

    }


    async function RenderAddNewForm(id)
    {        
        await $.ajax({
            url: config.apiUrl.base+'/api/hr/job/get/'+id,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            // data: {
            //     id : id
            // },
            headers: {"Authorization": "Bearer " + token},
            success: function(response) {
                console.log(response);
                if(response.status == true){
                    var res = response.data;
                   
                    $('#AddFormId [name=hr_job_id').val(res.hr_job_id);
                    $('#AddFormId [name=hr_job_title]').val(res.hr_job_title);
                    $('#AddFormId [name=hr_job_types]').val(res.hr_job_types);
                    $('#AddFormId [name=hr_job_start_date]').val(res.hr_job_start_date);
                    $('#AddFormId [name=hr_job_expire_date]').val(res.hr_job_expire_date);

                    // ตั้งค่า dept_status
                    if (res.hr_job_status === 'INACTIVE') {
                        $('#hr_job_status').prop('checked', false); // ตั้งค่าเป็น unchecked
                        $('#label_status').next('.form-check-label').text('In Active'); // เปลี่ยนข้อความ
                    } else {
                        $('#hr_job_status').prop('checked', true); // ตั้งค่าเป็น checked
                        $('#label_status').next('.form-check-label').text('Active'); // เปลี่ยนข้อความ
                    }                        
                }else{
                    MessageBox.ErrorMessage(response.code,response.description);
                    
                }
               
            },
            error: function(xhr, status, error) {
                
                console.error(xhr.responseText);
                if (xhr.status === 401) {
                    alert("Session expired. Redirecting to login page.");
                    window.location.href = 'login.html'; 
                    localStorage.removeItem("token");
                }
                alert('Error in form submission');
            }
        });

    }
    function ClearAddNew()
    {
        $('#AddFormId').trigger('reset');
        $('#AddFormId [name=id]').val('0');
        $('#saveId').prop('disabled', false);
        
       
    }


    
}); //end ready 


