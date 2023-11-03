import { Injectable } from '@angular/core';
import {HttpClient } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
import { carsModel } from '../models/carsModel';

@Injectable({
  providedIn: 'root'
})
export class CarServicesService {

  headers: any;
  list:carsModel[] = [];

  constructor(private http: HttpClient) { }

  refreshList(){
        this.headers = new Headers({
          'Content-Type': 'application/json',
          'Authorization': `Bearer ` + localStorage.getItem('token')
        })
        this.http.get(environment.apiBaseUrl + '/cars', {headers:this.headers})
        .subscribe({
          next: res => {
            this.list = res as carsModel[];
          },
          error: err => {console.log(err)}
        })
      }
}
