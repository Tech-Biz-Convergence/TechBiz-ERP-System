// Create tree object
var demoTreeData = {
  "Number": "DEP-100",
  "Name": "All Departments",
  "Children": [
    {
      "Number": "DEP-101",
      "Name": "Human Resources",
      "Children": [
        {
          "Number": "DEP-101-01",
          "Name": "Recruitment",
          "Children": []
        },
        {
          "Number": "DEP-101-02",
          "Name": "Employee Relations",
          "Children": []
        }
      ]
    },
    {
      "Number": "DEP-102",
      "Name": "Finance",
      "Children": [
        {
          "Number": "DEP-102-01",
          "Name": "Accounts Payable",
          "Children": []
        },
        {
          "Number": "DEP-102-02",
          "Name": "Accounts Receivable",
          "Children": []
        }
      ]
    },
    {
      "Number": "DEP-103",
      "Name": "IT",
      "Children": [
        {
          "Number": "DEP-103-01",
          "Name": "Infrastructure",
          "Children": []
        },
        {
          "Number": "DEP-103-02",
          "Name": "Software Development",
          "Children": []
        }
      ]
    }
  ]
};

// Initialize Simple Tree Picker
// Pass it an onclick function to update the view
// Pass it an initial selected state
$('.tree').simpleTreePicker({
  "tree": demoTreeData,
  "onclick": function () {
    var selected = $(".tree").simpleTreePicker("display");
    $("#selected").html(!!selected.length ? selected.toString().replace(/,/g, ', ') : "Nothing Selected");
  },
  "selected": ["ZZ-654-66", "SS-001-99"],
  "name": "room-selection-tree"
});

// Update view with initial state (onclick isn't called for initial selection)
$("#selected").html($(".tree").simpleTreePicker("display").toString().replace(/,/g, ', '));	
