/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function() {



    //============================================================================
    // ZONE : RUN INIT
    // create date   : 20240619 
    // create by     : Game
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
    console.log("username"+username);
    ShowMainListDataTable();

    
   
    //============================================================================
    // ZONE : SUBMIT
    // create date   : 20240619 
    // create by     : Game
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
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================

    $(document).on('click', '#logoutId', function() {
        console.log("Log out");
        window.location.href = 'login.html'; 
    });

    $(document).on('click', '#AddNewId', function() {
        $('#ToppicActionId').text('Add Candidate');
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
        $('#ToppicActionId').text('Edit Candidate');
        $('#saveId').prop('disabled', false);
        var id = $(this).attr('data-id');
        RenderAddNewForm(id);
        // console.log(formData);
        
        $('.SlideTabSearch').hide();
        $('#AreaAddId').show();
    });
    
    $(document).on('click', '.ViewButton',async function() {
        console.log("View");
        $('#ToppicActionId').text('View Candidate');
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
            url: config.apiUrl.base + '/Api/Hr/Candidates/Delete/' + id
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
                url: config.apiUrl.base + '/Api/Hr/Candidates/activateCondition' + id + '&user_id=' + loginId + '&is_active=false',
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
                url: config.apiUrl.base + '/Api/Hr/Candidates/activateCondition' + id + '&user_id=' + loginId + '&is_active=true',
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
    // create date   : 20240619 
    // create by     : Game
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
                                columns: [1,2,3,4],
                            }
                        },
                        {
                            extend: 'pdfHtml5',
                            download: 'open',
                            exportOptions: {
                                columns: [1,2,3,4],
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
                    
                    url: config.apiUrl.base+'/api/hr/candidates/GetPaginate',
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
                { targets: 0, data: 'hr_candidate_name', ordering: true },
                { targets: 1, data: 'hr_job_id', ordering: true },
                { targets: 2, data: 'mobile_number', ordering: true },
                { targets: 3, data: 'email', ordering: true },
                {
                    targets: 4,
                    data: null,
                    orderable: false,
                    className: 'dt-head-center dt-body-center',
                    render: function (data, type, row) {
                        return `<div class="row"><button type="button" class="btn btn btn-info btn-sm ViewButton col-4" data-id="${row.hr_candidate_id}" ><i class="bx bx-list-ul me-1"></i></button> ` +
                            ` <button type="button" class="btn btn-primary btn-sm EditButton col-4" data-id="${row.hr_candidate_id}"><i class="bx bx-edit me-1"></i></button> `+
                            ` <button type="button" class="btn btn-danger btn-sm DeleteButton col-4" data-id="${row.hr_candidate_id}"><i class="bx bx-trash me-1"></i></button> </div>`;
                    }
                }
            ]
          });

          
    } 

    function ShowUploadListDataTable(formData)
    {

        var formData = new FormData(this);

        $.ajax({
            url: config.apiUrl.base+'/api/hr/candidates/ImportDataExcelFile',
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
            case 0:
                return 'hr_candidate_name';
            case 1:
                return 'hr_job_id';
            case 2:
                return 'mobile_number';
            case 3:
                return 'email';
            default:
                return null;
        }
    }
    async function  AddFromSubmit(formData)
    {
        var type;
        var url;
       
        //check insert or update
        if (formData.hr_candidate_id == '0') {
            //insert 
            formData.created_by = username;
            var type = 'POST';
            var url =config.apiUrl.base+'/api/hr/candidates/addnew';
        }else{
            //update
            formData.updated_by = username;
            type = 'PUT';
            url = config.apiUrl.base+'/api/hr/candidates/update';
        }
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
            url: config.apiUrl.base+'/api/hr/candidates/get/'+id,
            type: 'GET',
            contentType: 'application/json; charset=utf-8',
            // data: {
            //     id : id
            // },
            headers: {"Authorization": "Bearer " + token},
            success: function(response) {
                console.log(response);
                // console.log(username);
                if(response.status == true){
                    var res = response.data;
                    $('#AddFormId [name=hr_candidate_id]').val(res.hr_candidate_id);
                    $('#AddFormId [name=hr_candidate_name]').val(res.hr_candidate_name);
                    $('#AddFormId [name=hr_job_id]').val(res.hr_job_id);
                    $('#AddFormId [name=mobile_number]').val(res.mobile_number);
                    $('#AddFormId [name=email]').val(res.email);
                    // $('#AddFormId [name=created_by]').val(username);
                    // $('#AddFormId [name=updated_by]').val(username);
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
        $('#AddFormId [name=hr_candidate_id]').val('0');
        $('#AddFormId [name=created_by]').val(username);
        // $('#AddFormId [name=updated_by]').val(username);
        $('#saveId').prop('disabled', false);
        
       
    }


    
}); //edn ready 

