/**
 * Account Settings - Account
 */

'use strict';
$(document).ready(function () {



  //============================================================================
  // ZONE : RUN INIT
  // create date   : 20240619 
  // create by     : Game
  // detail        :
  //============================================================================

  var Page = {
    mainTableControl: {}
  }
  const icon = "<i class='bx bx-alarm-exclamation me-2'></i> ";

  //get token
  const token = localStorage.getItem("token");
  const username = localStorage.getItem("user_name");

  ShowMainListDataTable();

  //============================================================================
  // ZONE : SUBMIT
  // create date   : 20240619 
  // create by     : Game
  // detail        :
  //============================================================================

  //>>>>>> Event Add PartNumber submit 
  $("#AddFormId").validate({
    rules: {
      emp_firstname: {
        required: true,
        maxlength: 255
      },
      emp_lastname: {
        required: true,
        maxlength: 255
      },
      emp_code: {
        required: true,
        maxlength: 50
      },
      emp_mobile_no: {
        maxlength: 10
      },
      start_date: {
        required: true,
      },
      dept_id: {
        required: true,
      }
    },
    messages: {
      emp_firstname: {
        required: icon + " Please enter employee firstname.",
        maxlength:  icon + " Please enter no more than 255 characters."
      },
      emp_lastname: {

        required: icon + " Please enter employee lastname.",
        maxlength: icon + " Please enter no more than 255 characters."
      },
      emp_code: {
        required: icon + " Please enter employee code.", 
        maxlength: icon + " Please enter no more than 50 characters."
      },
      emp_mobile_no: {
        maxlength: icon + " Please enter no more than 10 characters."
      },
      start_date: {
        required: icon + " Please select start date.", 
      } ,
      dept_id: {
        required: icon + " Please select department.",
      }
    },
    errorPlacement: (error, element) => {
      if (element.hasClass('select2-hidden-accessible')) {
        error.addClass("help-block small");
        error.insertAfter(element.next('.select2-container')); // ใส่ error ใต้ select2-container
      } else {
        error.addClass("help-block small");
        error.insertAfter(element);
      }
    },
    highlight: (element, errorClass, validClass) => {
      $(element).closest('div').addClass('has-error').removeClass('has-success');
    },
    unhighlight: (element, errorClass, validClass) => {
      $(element).closest('div').removeClass('has-error');
    },
    submitHandler: async (form) => {
      event.preventDefault();
      var formData = $(form).serializeObject();
      console.log(formData);
      AddFromSubmit(formData);
    }
  });


  $('#UploadFormId').submit(async function (event) {
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
  $('#export-excel').on('click', function () {
    Page.mainTableControl.button('.buttons-excel').trigger();
  });

  $('#export-pdf').on('click', function () {
    Page.mainTableControl.button('.buttons-pdf').trigger();
  });
  $('#export-csv').on('click', function () {
    Page.mainTableControl.button('.buttons-csv').trigger();
  });

  $('#export-copy').on('click', function () {
    Page.mainTableControl.button('.buttons-copy').trigger();
  });


  $(document).on('click', '#AddNewId', function () {
    ClearAddNew();
    $('#ToppicActionId').text('Add Employee');
    $('.SlideTabSearch').hide();
    $('#AreaAddId').show();
  });

  $(document).on('click', '#CloseId', function () {
    $('.SlideTabSearch').hide();
    $('#AreaSearchId').show();
  });



  $(document).on('click', '.EditButton', function () {
    debugger;
    console.log("Edit");
    $('#ToppicActionId').text('Edit Employee');
    $('#saveId').prop('disabled', false);
    var id = $(this).attr('data-id');
    console.log('id:' + id);
    RenderAddNewForm(id);


    $('.SlideTabSearch').hide();
    $('#AreaAddId').show();
  });

  $(document).on('click', '.ViewButton', async function () {
    console.log("View");
    $('#ToppicActionId').text('View Employee');
    $('#saveId').prop('disabled', true);
    // alert("Test");
    var id = $(this).attr('data-id');
    RenderAddNewForm(id);
    $('.SlideTabSearch').hide();
    $('#AreaAddId').show();

  });

  $(document).on('click', '.DeleteButton', function () {
    MessageBox.ConfirmDeletedMessage("", () => {
      var id = $(this).attr('data-id');
      return $.ajax({
        type: 'DELETE',
        headers: { "Authorization": "Bearer " + token },
        contentType: 'application/json; charset=utf-8',
        url: config.apiUrl.base + '/Api/Auth/Employee/Delete/' + id,
        data: {
          user_name: username
        }
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

    var urlApi = "";
    var status = "";

    $('body').addClass('loading');
    if (button.hasClass('btn-primary')) {
      console.log(" hasClass('btn-primary') ");
      button.removeClass('btn-primary').addClass('btn-outline-secondary');
      urlApi = config.apiUrl.base + '/Api/Auth/Employee/activateCondition/' + id;
      status = "INACTIVE";
      console.log('urlApi : ' + urlApi);
    } else {
      console.log(" No hasClass('btn-primary') ");
      button.removeClass('btn-outline-secondary').addClass('btn-primary');
      urlApi = config.apiUrl.base + '/Api/Auth/Employee/activateCondition/' + id;
      status = "ACTIVE";
      console.log('urlApi : ' + urlApi);
    }
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
    console.log("time : " + time);

    $.ajax({
      url: urlApi,
      type: 'GET',
      contentType: 'application/json; charset=utf-8',
      data: {
        user_name: username,
        emp_status: status
      },
      headers: { "Authorization": "Bearer " + token },
      success: function (response) {
        console.log(response);

        if (response.status != true) {
          alert("Error Code : " + response.code + " Error Description " + response.description);
          MessageBox.ErrorMessage(response.code, response.description);
        }

      },
      error: function (xhr, status, error) {

        console.error(xhr.responseText);
        if (xhr.status === 401) {
          alert("Session expired. Redirecting to login page.");
          window.location.href = window.location.origin + '/Auth/LoginBasic';
          localStorage.removeItem("token");
        }
        alert('Error in form submission');
      }
    });

  });


  $(document).on('click', '#UploadCloseId', function () {
    $('.SlideTabUpload').hide();
    $('#AreaUploadId').show();
  });

  //============================================================================
  // ZONE : CUSTOM FUNCTION
  // create date   : 20240619 
  // create by     : Game
  // detail        :
  //============================================================================

  function ShowMainListDataTable() {
    Page.mainTableControl = $('#tableShowListId').DataTable({
      layout: {
        top1Start: {
          buttons: [
            {
              extend: 'copyHtml5',
              className: 'd-none'
            },
            {
              extend: 'print',
              className: 'd-none',
              exportOptions: {
                columns: [1, 2, 3, 4],
              }
            },
            {
              extend: 'csvHtml5',
              className: 'd-none'
            },
            {
              extend: 'excelHtml5',
              className: 'd-none',
              autoFilter: true,
              sheetName: 'Exported data',
              exportOptions: {
                columns: [1, 2, 3, 4],
              }
            },
            {
              extend: 'pdfHtml5',
              className: 'd-none',
              download: 'open',
              exportOptions: {
                columns: [1, 2, 3, 4],
              }
            }
          ]
        }
      },
      responsive: true,
      searching: true,
      paging: true,
      serverSide: true,
      ajax: (data, callback) => {
        $.ajax({

          url: config.apiUrl.base + '/api/auth/employee/GetPaginate',
          type: 'GET',
          contentType: 'application/json; charset=utf-8',
          headers: { "Authorization": "Bearer " + token },
          beforeSend: () => {
            $('body').addClass('loading');
          },
          data: {
            page: Math.ceil(data.start / data.length) + 1,
            limit: data.length,
            sortBy: getTableSortBy(data.order[0].column),
            sortType: data.order[0].dir,
            searchValue: data.search.value
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
      aaSorting: [[1, 'desc']],
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
            // ตรวจสอบค่า row.emp_status
            var buttonClass = row.emp_status === 'ACTIVE' ? 'btn-primary' : 'btn-outline-secondary';
            // สร้าง HTML ของปุ่ม
            return `<button type="button" class="btn rounded-pill btn-icon ${buttonClass} ActiveButton button-feature " data-id="${row.emp_id}">
                              <span class="tf-icons bx bx-power-off"></span>
                        </button>`;
          }
        },
        { targets: 1, data: 'emp_code', ordering: true },
        {
          targets: 2,
          render: function (data, type, row) {
            return row.emp_firstname + ' ' + row.emp_lastname; // รวม firstName และ lastName เข้าด้วยกัน
          },
          ordering: true
        },
        { targets: 3, data: 'emp_mobile_no', ordering: true },
        {
          targets: 4,
          data: 'start_date',
          render: function (data, type, row, meta) {
            // แปลงรูปแบบวันที่ตามที่ต้องการ
            return formatDate(row.start_date)
          },
          ordering: true
        },
        {
          targets: 5,
          data: 'end_date',
          render: function (data, type, row, meta) {
            // แปลงรูปแบบวันที่ตามที่ต้องการ
            return formatDate(row.end_date)
          },
          ordering: true
        },
        { targets: 6, data: 'dept_name', ordering: true },
        {
          targets: 7,
          data: null,
          orderable: false,
          className: 'dt-head-center dt-body-center',
          render: function (data, type, row) {
            return `<div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                              <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                            <a class="dropdown-item ViewButton" href="javascript:void(0);" data-id="${row.emp_id}" 
                                ><i class="bx bx-list-ul me-2"></i> View</a
                              >
                              <a class="dropdown-item EditButton" href="javascript:void(0);" data-id="${row.emp_id}" 
                                ><i class="bx bx-edit-alt me-2"></i> Edit</a
                              >
                              <a class="dropdown-item DeleteButton" href="javascript:void(0);" data-id="${row.emp_id}" 
                                ><i class="bx bx-trash me-2"></i> Delete</a
                              >
                            </div>
                          </div>`;
          }
        }
      ]
    });


  }

  function ShowUploadListDataTable(formData) {

    var formData = new FormData(this);

    $.ajax({
      url: config.apiUrl.base + '/api/auth/employee/ImportDataExcelFile',
      headers: { "Authorization": "Bearer " + token },
      type: 'POST',
      data: formData,
      processData: false,
      contentType: false,
      success: function (response) {
        alert('File uploaded successfully.');
      },
      error: function (jqXHR, textStatus, errorThrown) {
        alert('Error uploading file: ' + textStatus);
      }
    });


  }
  function getTableSortBy(column) {
    switch (column) {
      case 1:
        return 'emp_code';
      case 2:
        return 'emp_firstname';
      case 3:
        return 'emp_mobile_no';
      case 4:
        return 'start_date';
      case 5:
        return 'end_date';
      case 6:
        return 'dep_name';
      default:
        return null;
    }
  }

 

  async function AddFromSubmit(formData) {
    var type;
    var url;
  
    if (formData.end_date === "") {
      formData.end_date = null;
    }
    if ($('#AddFormId [name=emp_status]').prop('checked')) {
      formData.emp_status = 'ACTIVE'
    } else {
      formData.emp_status = 'INACTIVE'
    }

    //check insert or update
    if (formData.emp_id == '0') {

     
      //insert
      formData.create_by = username;
      var type = 'POST';
      var url = config.apiUrl.base + '/api/auth/employee/addnew';
    } else {
      //update
      formData.update_by = username;
      type = 'PUT';
      url = config.apiUrl.base + '/api/auth/employee/update';
    }
    await $.ajax({
      url: url,
      type: type,
      contentType: 'application/json; charset=utf-8',
      data: JSON.stringify(formData) ,
      headers: { "Authorization": "Bearer " + token },
      success: function (response) {
        console.log(response);

        if (response.status == true) {
          $('#AddFormId')[0].reset();
          MessageBox.SuccessMessage(response.code, response.description);

        } else {
          alert("Error Code : " + response.code + " Error Description " + response.description);
          MessageBox.ErrorMessage(response.code, response.description);
        }

      },
      error: function (xhr, status, error) {
        MessageBox.ErrorMessage("Error", xhr.responseText);
        console.error(xhr.responseText);
        if (xhr.status === 401) {
          alert("Session expired. Redirecting to login page.");
          window.location.href = window.location.origin + '/Auth/LoginBasic';
          localStorage.removeItem("token");
        }
       
      }
    });

    $('.SlideTabSearch').hide();
    $('#AreaSearchId').show();

    Page.mainTableControl.ajax.reload();

  }


  async function RenderAddNewForm(id) {
    await $.ajax({
      url: config.apiUrl.base + '/api/auth/employee/get/' + id,
      type: 'GET',
      contentType: 'application/json; charset=utf-8',
      // data: {
      //     id : id
      // },
      headers: { "Authorization": "Bearer " + token },
      success: function (response) {
        console.log(response);
        if (response.status == true) {
          debugger;
          var res = response.data;
          $('#AddFormId [name=emp_id]').val(res.emp_id);
          $('#AddFormId [name=emp_code]').val(res.emp_code);
          $('#AddFormId [name=emp_firstname]').val(res.emp_firstname);
          $('#AddFormId [name=emp_lastname]').val(res.emp_lastname);
          $('#AddFormId [name=emp_mobile_no]').val(res.emp_mobile_no);
          $('#AddFormId [name=start_date]').val(formatDate(res.start_date));
          $('#AddFormId [name=end_date]').val(formatDate(res.end_date));
          $('#AddFormId [name=create_date]').val(res.create_date);
          $('#AddFormId [name=create_by]').val(res.create_by);
          $('#AddFormId [name=update_date]').val(res.update_date);
          $('#AddFormId [name=update_by]').val(res.update_by);
          if (res.emp_status === "ACTIVE") {
            $('#AddFormId [name=emp_status]').prop('checked', true); // ตั้งค่าให้ checkbox มีสถานะ checked
          } else {
            $('#AddFormId [name=emp_status]').prop('checked', false); // ตั้งค่าให้ checkbox ไม่มีสถานะ checked
          }
          $('#AddFormId [name=dept_id]').append(new Option(res.dept_name, res.dept_id, true, true)).trigger('change');




        } else {
          MessageBox.ErrorMessage(response.code, response.description);

        }

      },
      error: function (xhr, status, error) {

        console.error(xhr.responseText);
        if (xhr.status === 401) {
          alert("Session expired. Redirecting to login page.");
          window.location.href = window.location.origin + '/Auth/LoginBasic';
          localStorage.removeItem("token");
        }
        alert('Error in form submission');
      }
    });

  }
  function ClearAddNew() {
    $('#AddFormId').trigger('reset');
    $('#AddFormId [name=emp_id]').val('0');
    $('#AddFormId div.has-error').removeClass('has-error');
    $('#AddFormId label.help-block').remove();
    $('#saveId').prop('disabled', false);
    $('#AddFormId select').val('');
    $('#AddFormId select').trigger('change');

  }



}); //edn ready 


