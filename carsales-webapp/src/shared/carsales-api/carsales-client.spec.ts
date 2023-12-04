
import { CarSalesClient } from "./carsales-client";
import { TestBed } from "@angular/core/testing";
import { HttpClientTestingModule} from '@angular/common/http/testing';

describe('CarSalesClient', () => {
    let carSalesClient: CarSalesClient;

    beforeEach(()=>{
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [CarSalesClient]
        })

        carSalesClient = TestBed.inject(CarSalesClient);
    })

    it('should be created', ()=>{
        expect(carSalesClient).toBeTruthy();
    })

})