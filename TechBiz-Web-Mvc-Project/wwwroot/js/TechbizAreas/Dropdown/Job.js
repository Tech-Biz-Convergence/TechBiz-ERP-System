'use strict';
$(document).ready(function () {

  debugger;
  SetJobDDL();
  function SetJobDDL() {
    $('#AddFormId [name=hr_job_id]').select2({
      tags: true,
      //dropdownParent: $('#assyPartControlModal'),
      theme: 'bootstrap-5',
      placeholder: 'Select Job',
      //allowClear: true
      ajax: {
        url: config.apiUrl.base + '/Api/hr/Job/GetJobName',
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
                  id: item.hr_job_id,
                  text: item.hr_job_title
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
