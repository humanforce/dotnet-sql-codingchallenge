import { NgModule} from '@angular/core'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SalesPageRoutingModule } from './sales.page.routing';
import { SalesPage } from './sales.page'

import { CarSalesModule } from '../../../shared/carsales-api'

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SalesPageRoutingModule,
        CarSalesModule
    ],
    declarations: [
        SalesPage
    ]
})
export class SalesPageModule { }