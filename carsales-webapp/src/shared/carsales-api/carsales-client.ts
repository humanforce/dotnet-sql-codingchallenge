import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ICarSalesClient } from './carsales-client.interface'
import { CarSale } from './models';

@Injectable({
    providedIn: 'any'
})
export class CarSalesClient implements ICarSalesClient
{
    constructor(private httpClient:HttpClient){
    }

    getSales(startDate:Date|null|undefined, endDate:Date|null|undefined) : Observable<Array<CarSale>>
    {
        var params = new HttpParams();

        if(startDate instanceof Date){
            params = params.set('startDate', startDate.toISOString().slice(0, 10).replace(/-/g, ''));
        }

        if(endDate instanceof Date)
        {
            params = params.set('endDate', endDate.toISOString().slice(0, 10).replace(/-/g, ''));
        }
        console.log(params);
        return this.httpClient.get<Array<CarSale>>('http://localhost:5195/sales', {params})
    }
}