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
      job_id: {
        required: true,
      },
      interview_quest: {
        required: true,
        maxlength: 500
      }
    },
    messages: {
      job_id: {
        required: icon + " Please select Job.",
      },
      interview_quest: {
        required: icon + " Please enter question.",
        maxlength: icon + " Please enter no more than 500 characters."
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
    $('#ToppicActionId').text('Add Interview');
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
    $('#ToppicActionId').text('Edit Interview');
    $('#saveId').prop('disabled', false);
    var id = $(this).attr('data-id');
    console.log('id:' + id);
    RenderAddNewForm(id);


    $('.SlideTabSearch').hide();
    $('#AreaAddId').show();
  });

  $(document).on('click', '.ViewButton', async function () {
    console.log("View");
    $('#ToppicActionId').text('View Interview');
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
        url: config.apiUrl.base + '/api/hr/Interview/Delete/' + id,
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
      urlApi = config.apiUrl.base + '/Api/Hr/Interview/activateCondition/' + id;
      status = "INACTIVE";
      console.log('urlApi : ' + urlApi);
    } else {
      console.log(" No hasClass('btn-primary') ");
      button.removeClass('btn-outline-secondary').addClass('btn-primary');
      urlApi = config.apiUrl.base + '/Api/Hr/Interview/activateCondition/' + id;
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
        holiday_status: status
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

          url: config.apiUrl.base + '/api/hr/interview/GetPaginate',
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
            // ตรวจสอบค่า row.interview_status
            var buttonClass = row.interview_status === 'ACTIVE' ? 'btn-primary' : 'btn-outline-secondary';
            // สร้าง HTML ของปุ่ม
            return `<button type="button" class="btn rounded-pill btn-icon ${buttonClass} ActiveButton button-feature " data-id="${row.interview_id}">
                              <span class="tf-icons bx bx-power-off"></span>
                        </button>`;
          }
        },
        { targets: 1, data: 'hr_job_title', ordering: true },
        { targets: 2, data: 'interview_quest', ordering: true },
        {
          targets: 3,
          data: null,
          orderable: false,
          className: 'dt-head-center dt-body-center',
          render: function (data, type, row) {
            return `<div class="dropdown">
                            <button type="button" class="btn p-0 dropdown-toggle hide-arrow" data-bs-toggle="dropdown">
                              <i class="bx bx-dots-vertical-rounded"></i>
                            </button>
                            <div class="dropdown-menu">
                            <a class="dropdown-item ViewButton" href="javascript:void(0);" data-id="${row.interview_id}" 
                                ><i class="bx bx-list-ul me-2"></i> View</a
                              >
                              <a class="dropdown-item EditButton" href="javascript:void(0);" data-id="${row.interview_id}" 
                                ><i class="bx bx-edit-alt me-2"></i> Edit</a
                              >
                              <a class="dropdown-item DeleteButton" href="javascript:void(0);" data-id="${row.interview_id}" 
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
      url: config.apiUrl.base + '/api/hr/interview/ImportDataExcelFile',
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
        return 'hr_job_title';
      case 2:
        return 'interview_quest';
      default:
        return null;
    }
  }



  async function AddFromSubmit(formData) {
    var type;
    var url;

    if ($('#AddFormId [name=interview_status]').prop('checked')) {
      formData.interview_status = 'ACTIVE'
    } else {
      formData.interview_status = 'INACTIVE'
    }

    //check insert or update
    if (formData.interview_id == '0') {


      //insert
      formData.created_by = username;
      var type = 'POST';
      var url = config.apiUrl.base + '/api/hr/interview/addnew';
    } else {
      //update
      formData.updated_by = username;
      type = 'PUT';
      url = config.apiUrl.base + '/api/hr/interview/update';
    }
    await $.ajax({
      url: url,
      type: type,
      contentType: 'application/json; charset=utf-8',
      data: JSON.stringify(formData),
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
      url: config.apiUrl.base + '/api/hr/interview/get/' + id,
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
          $('#AddFormId [name=interview_id]').val(res.interview_id);
          $('#AddFormId [name=job_id]').val(res.job_id);
          $('#AddFormId [name=hr_job_title]').val(res.hr_job_title);
          $('#AddFormId [name=interview_quest]').val(res.interview_quest);
          $('#AddFormId [name=created_date]').val(formatDateTime(res.created_date));
          $('#AddFormId [name=created_by]').val(res.created_by);
          $('#AddFormId [name=updated_date]').val(formatDateTime(res.updated_date));
          $('#AddFormId [name=updated_by]').val(res.updated_by);
          if (res.interview_status === "ACTIVE") {
            $('#AddFormId [name=interview_status]').prop('checked', true); // ตั้งค่าให้ checkbox มีสถานะ checked
          } else {
            $('#AddFormId [name=interview_status]').prop('checked', false); // ตั้งค่าให้ checkbox ไม่มีสถานะ checked
          }
          //$('#AddFormId [name=dept_id]').append(new Option(res.dept_name, res.dept_id, true, true)).trigger('change');




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
    $('#AddFormId [name=interview_id]').val('0');
    $('#AddFormId div.has-error').removeClass('has-error');
    $('#AddFormId label.help-block').remove();
    $('#saveId').prop('disabled', false);
    $('#AddFormId select').val('');
    $('#AddFormId select').trigger('change');

  }



}); //edn ready 


