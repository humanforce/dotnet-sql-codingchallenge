import { CarSale } from './models';
import { Observable } from 'rxjs'

export abstract class ICarSalesClient {
    abstract getSales(startDate:Date|null|undefined, endDate:Date|null|undefined) : Observable<Array<CarSale>>;
}