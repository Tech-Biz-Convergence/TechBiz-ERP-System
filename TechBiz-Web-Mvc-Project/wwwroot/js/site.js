// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// custom-functions.js

// custom by game 20240711
$.fn.serializeObject = function () {
  var result = {};
  var formData = this.serializeArray();
  $.each(formData, function () {
    if (result[this.name]) {
      if (!result[this.name].push) {
        result[this.name] = [result[this.name]];
      }
      result[this.name].push(this.value || '');
    } else {
      result[this.name] = this.value || '';
    }
  });
  return result;
};


class MessageBox {
  static SuccessMessage(code, description) {
    return Swal.fire({
      title: code,
      text: description,
      icon: "success"
    });
  }

  static ErrorMessage(code, description) {
    return Swal.fire({
      title: code,
      text: description,
      icon: "error"
    });

  }

  static ConfirmDeletedMessage(message, callback) {
    return Swal.fire({
      title: "Are you sure?",
      text: message,
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "Yes, delete it!"
    }).then((result) => {
      if (result.isConfirmed) {
        if (callback) {
          return callback().then((success) => {
            if (success) {
              Swal.fire({
                title: "Deleted!",
                text: "Your data has been deleted.",
                icon: "success"
              });
            } else {
              Swal.fire({
                title: "Error!",
                text: "There was a problem deleting your data.",
                icon: "error"
              });
            }
            return success;
          }).catch((error) => {
            Swal.fire({
              title: "Error!",
              text: "There was a problem deleting your data.",
              icon: "error"
            });
            return false;
          });
        }
      }
      return false;
    });
  }
}

function formatDate(date) {
  const d = new Date(date);
  let month = '' + (d.getMonth() + 1);
  let day = '' + d.getDate();
  const year = d.getFullYear();  

  if (month.length < 2) month = '0' + month;
  if (day.length < 2) day = '0' + day;

  return [year, month, day].join('-');
}

function formatDateTime(date) {
  const d = new Date(date);
  let month = '' + (d.getMonth() + 1);
  let day = '' + d.getDate();
  const year = d.getFullYear();

  let t = d.toLocaleTimeString().toLowerCase();

  if (month.length < 2) month = '0' + month;
  if (day.length < 2) day = '0' + day;

  return [year, month, day].join('-') + " " + t;
}

document.addEventListener('DOMContentLoaded', function () {
  var saveButton = document.getElementById('saveId');
  var closeButton = document.getElementById('CloseId');
  saveButton.className = '';
  closeButton.className = '';
  if (saveButton) {
    saveButton.classList.add('btn');
    saveButton.classList.add('btn-success');
    saveButton.classList.add('rounded-pill');
    saveButton.classList.add('me-2');
  }

  if (closeButton) {
    closeButton.classList.add('btn-danger');
    closeButton.classList.add('rounded-pill');
    closeButton.classList.add('btn');
  }
});
