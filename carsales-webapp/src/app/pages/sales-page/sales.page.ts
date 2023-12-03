import {Component, OnInit} from '@angular/core'
import { FormControl, FormGroup } from '@angular/forms'
import { CarSale, ICarSalesClient } from '../../../shared/carsales-api';

@Component({
    selector: 'sales-page',
    templateUrl: 'sales.page.html',
    styleUrls: ['sales.page.scss']
})
export class SalesPage
{
    public searchForm: FormGroup;
    public sales: Array<CarSale> = [];

    constructor(private carSalesClient: ICarSalesClient)
    {
        this.searchForm = new FormGroup({
            startDate: new FormControl(),
            endDate: new FormControl()
        });
    }

    onSubmit() {
        var startDate = this.searchForm.get('startDate')?.value;
        var endDate = this.searchForm.get('endDate')?.value;
        console.log(`startDate: ${startDate} | ${typeof(startDate)}`);
        console.log(`endDate: ${endDate} | ${typeof(endDate)}`);

        this.carSalesClient.getSales(
            typeof(startDate) === 'string' ? new Date(startDate) : null,
            typeof(endDate) === 'string' ? new Date(endDate) : null
        ).subscribe((data)=> {
            this.sales = data;
        })
    }
}