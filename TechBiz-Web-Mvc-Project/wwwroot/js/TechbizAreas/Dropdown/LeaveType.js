'use strict';
$(document).ready(function () {

  debugger;
  SetDepartmentDDL();
  function SetDepartmentDDL() {
    $('#AddFormId [name=leave_type_id]').select2({
      tags: true,
      //dropdownParent: $('#assyPartControlModal'),
      theme: 'bootstrap-5',
      placeholder: 'Select Leave Type',
      //allowClear: true
      ajax: {
        url: config.apiUrl.base + '/Api/Hr/ListLeave/GetLeaveType',
        type: 'GET',
        dataType: 'json',
        headers: { "Authorization": "Bearer " + token },
        data: (params) => {

          return {
            searchValue: params.term,  //term Ther current serach term in ther serach box.
            page: params.page || 1,
            pageLimit: 10
          };
        },
        processResults: (data) => {

          // ตรวจสอบว่า data.data มีค่าและเป็น array
          if (data && Array.isArray(data.data)) {
            return {
              results: data.data.map(item => {
                return {
                  id: item.leave_type_id,
                  text: item.leave_type_name
                };
              }),
              pagination: {
                more: data.data.length === 10
                // more: data.nextPage // ใช้ถ้ามีการจัดการ pagination
              }
            };
          } else {
            // หากไม่พบข้อมูลที่คาดหวัง ให้คืนค่าที่เหมาะสม
            return {
              results: [],
              pagination: {
                more: false
              }
            };
          }
        }
      },
      language: {
        searching: () => {
          return "Loading...";
        },
        loadingMore: () => {
          return "Loading more...";
        }
      }
    });
  }

  
});
