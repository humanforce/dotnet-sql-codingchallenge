import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders  } from "@angular/common/http";
import { environment } from 'src/environments/environment.development';
@Injectable({
  providedIn: 'root'
})
export class LoginServicesService {

  constructor(private http: HttpClient) { }

  buildHeader(){
    const payload = {
      "UserName": "admin",
      "Password": "admin",
      "Role": "admin",
      "Token": "2323"
    };

    this.http.post(environment.apiBaseUrl + '/user/login', payload, {
      responseType: 'text'
  })
    .subscribe({
      next: res => {
      localStorage.setItem('token', res)
      },
      error: err => {console.log(err)}
    })
  }
}
