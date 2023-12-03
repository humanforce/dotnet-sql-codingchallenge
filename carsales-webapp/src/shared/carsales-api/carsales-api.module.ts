import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http'
import {CarSalesClient} from './carsales-client'
import {ICarSalesClient} from './carsales-client.interface'

@NgModule({
    imports: [HttpClientModule],
    providers: [{provide: ICarSalesClient, useClass: CarSalesClient}]
})
export class CarSalesModule {
}