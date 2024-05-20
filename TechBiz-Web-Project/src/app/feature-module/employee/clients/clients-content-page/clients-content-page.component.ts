import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService, clientsDatas, companiesList, routes } from 'src/app/core/core.index';
import { Employee } from '../clients.type';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-clients-content-page',
  templateUrl: './clients-content-page.component.html',
  styleUrls: ['./clients-content-page.component.scss'],
})
export class ClientsContentPageComponent implements OnInit{

  public companiesList: Array<companiesList>;
  public routes = routes;
  public clientsData: Array<clientsDatas>;
  public employeeData: Array<Employee> = [];
  constructor(public router: Router, private dataservice: DataService,private http : HttpClient ) {
    this.companiesList = this.dataservice.companiesList;
    this.clientsData = this.dataservice.clientsDatas;
  }
  selected1 = 'option1';

  ngOnInit():void{
    this.fetchData();
  }

  fetchData(){
    this.http.get<{ status: boolean, code: any, description: any, createdBy: number, data: Employee[] }>("https://localhost:7087/api/Employee/GetEmployee")
    .subscribe(response => {
      console.log("response data", response.data);
      this.employeeData = response.data; // เก็บข้อมูลในตัวแปรของคลาส
    });
  }

}
