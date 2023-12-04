import { Component, OnInit } from '@angular/core';
import { SaleServicesService } from 'src/app/services/sale-services.service';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit 
{
  todayDate:string = "";
  constructor(public service: SaleServicesService){
 }
  
 public changedDate(eventData: Event){
  this.service.refreshList((<HTMLInputElement>eventData.target).value);
}

  ngOnInit(): void{
    const currDate = new Date();
    this.todayDate = currDate.getFullYear() + "-" + (currDate.getMonth() + 1);
    console.log( this.todayDate);
    this.service.refreshList("");
    // this.service.refreshList();
  }
}
