import { Injectable } from '@angular/core';
import { salesModel } from '../models/salesModel';
import {HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { CurrencyPipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class SaleServicesService {

  headers: any;
  list:salesModel[] = [];
  
  constructor(private http: HttpClient) { }

  refreshList(date: String){

    var month: Number = 0;
    var year: number = 0;
    const currDate = new Date();
    if (date != null && date != ""){

      var dateArr = date.split("-");
      const comdate = dateArr[0] + "/" + dateArr[1] + "/01";  
      const inputDate = new Date(comdate);

      month = inputDate.getMonth() + 1;
      year = inputDate.getFullYear();
    }else{

      month = currDate.getMonth() + 1;
      year = currDate.getFullYear();
    }

        this.headers = new Headers({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ` + localStorage.getItem('token')
        })
        this.http.get(environment.apiBaseUrl + '/sales?month=' + month.toString() + '&year=' + year.toString(), {headers:this.headers})
        .subscribe({
          next: res => {
            this.list = res as salesModel[];
          },
          error: err => {console.log(err)}
        })
      }
}
