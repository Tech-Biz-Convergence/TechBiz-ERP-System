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
    if (!token) {
        window.location.href= 'login.html';
    }
    $('.Slide').hide();
    $('#SectionSearchId').show();
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
    
    //============================================================================
    // ZONE : EVENT CLICK
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================

    $("#logoutId").click(function(){
        console.log("Log out");
        window.location.href = 'login.html'; 
    });

    $("#AddNewId").click(function(){
        $('#ToppicActionId').text('Add Employee');
        ClearAddNew();
        $('.Slide').hide();
        $('#SectionAddId').show();
    });


    $("#CloseId").click(function(){
        $('.Slide').hide();
        $('#SectionSearchId').show();
    });

    $(document).on('click', '.EditButton', function() {
        console.log("Edit");
        $('#ToppicActionId').text('Edit Employee');
        $('#saveId').prop('disabled', false);
        var id = $(this).attr('data-id');
        RenderAddNewForm(id);

        $('.Slide').hide();
        $('#SectionAddId').show();
    });
    
    $(document).on('click', '.ViewButton',async function() {
        console.log("View");
        $('#ToppicActionId').text('View Employee');
        $('#saveId').prop('disabled', true);
        // alert("Test");
        var id = $(this).attr('data-id');
        RenderAddNewForm(id);
        $('.Slide').hide();
        $('#SectionAddId').show();
       
    });
    
    $(document).on('click', '.DeleteButton', function() {
        MessageBox.ConfirmDeletedMessage("", () => {
          var id = $(this).attr('data-id');
          return $.ajax({
            type: 'DELETE',
            headers: {"Authorization": "Bearer " + token},
            contentType: 'application/json; charset=utf-8',
            url: config.apiUrl.base + '/Api/Auth/Employee/Delete/' + id
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
                url: config.apiUrl.base + '/Api/Auth/Employee/activateCondition' + id + '&user_id=' + loginId + '&is_active=false',
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
                url: config.apiUrl.base + '/Api/Auth/Employee/activateCondition' + id + '&user_id=' + loginId + '&is_active=true',
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


    //============================================================================
    // ZONE : CUSTOM FUNCTION
    // create date   : 20240619 
    // create by     : Game
    // detail        :
    //============================================================================

    function ShowMainListDataTable()
    {
          Page.mainTableControl = $('#tableShowListId').DataTable({
            responsive: true,
            searching: true,
            paging: true,
            serverSide: true,
            ajax: (data, callback) => {
                $.ajax({
                    
                    url: config.apiUrl.base+'/api/auth/employee/GetPaginate',
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
                { targets: 1, data: 'name', ordering: true },
                { targets: 2, data: 'position', ordering: true },
                { targets: 3, data: 'department', ordering: true },
                { targets: 4, data: 'salary', ordering: true },
                {
                    targets: 5,
                    data: null,
                    orderable: false,
                    className: 'dt-head-center dt-body-center',
                    render: function (data, type, row) {
                        return `<button type="button" class="btn btn btn-info btn-sm ViewButton" data-id="${row.id}" ><i class="bx bx-list-ul me-1"></i></button> ` +
                            `<button type="button" class="btn btn-primary btn-sm EditButton" data-id="${row.id}"><i class="bx bx-edit me-1"></i></button> `+
                            `<button type="button" class="btn btn-danger btn-sm DeleteButton" data-id="${row.id}"><i class="bx bx-trash me-1"></i></button> `;
                    }
                }
            ]
          });
    } 
    
    function getTableSortBy(column){
        switch (column) {
            case 1:
                return 'name';
            case 2:
                return 'position';
            case 3:
                return 'department';
            case 4:
            return 'salary';
            default:
                return null;
        }
    }
    async function  AddFromSubmit(formData)
    {
        var type = 'POST';
        var url =config.apiUrl.base+'/api/auth/employee/addnew';
        var jsonData = JSON.parse(formData);
        //check insert or update
        if (jsonData.id != '0') {
            type = 'PUT';
            url = config.apiUrl.base+'/api/auth/employee/update';
        }
        await $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            data: formData,
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
        
        $('.Slide').hide();
        $('#SectionSearchId').show();
        Page.mainTableControl.ajax.reload();

    }

    async function RenderAddNewForm(id)
    {
        await $.ajax({
            url: config.apiUrl.base+'/api/auth/employee/get/'+id,
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
                    $('#AddFormId [name=id]').val(res.id);
                    $('#AddFormId [name=name]').val(res.name);
                    $('#AddFormId [name=position]').val(res.position);
                    $('#AddFormId [name=department]').val(res.department);
                    $('#AddFormId [name=salary]').val(res.salary);
                    
        

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


    
}); //edn ready 


