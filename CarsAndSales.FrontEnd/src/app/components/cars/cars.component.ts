import { Component, OnInit } from '@angular/core';
import { CarServicesService } from 'src/app/services/car-services.service';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {
  constructor(public service: CarServicesService){

  }
  ngOnInit(): void{
    this.service.refreshList();
  }
}
