import { Component, OnInit } from '@angular/core';
import { LoginServicesService } from './services/login-services.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'CarsAndSales.FrontEnd';
  constructor(public service: LoginServicesService){

  }
  ngOnInit(): void{
    this.service.buildHeader();
  }
}
