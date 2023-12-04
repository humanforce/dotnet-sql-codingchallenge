import {ComponentFixture, TestBed, TestComponentRenderer, fakeAsync} from '@angular/core/testing'
import {FormsModule, ReactiveFormsModule} from '@angular/forms'
import { By } from '@angular/platform-browser';
import { of} from 'rxjs';

import {SalesPage} from './sales.page'
import { CarSale, ICarSalesClient } from '../../../shared/carsales-api';


describe('SalesPage', () => {
    let component: SalesPage;
    let fixture: ComponentFixture<SalesPage>
    let mockCarSalesClient:any;
    let mockData : CarSale[];

    beforeEach(() => {
        mockCarSalesClient = jasmine.createSpyObj('ICarSalesClient', ['getSales']);
        TestBed.configureTestingModule({
            declarations : [SalesPage],
            imports : [FormsModule, ReactiveFormsModule],
            providers: [{provide: ICarSalesClient, useValue: mockCarSalesClient}]
        });

        fixture = TestBed.createComponent(SalesPage);
        component = fixture.componentInstance;

        mockData = [
            { carName : '', carColour: '', month: 2, year: 2023, quantity : 5}
        ]
    });


    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should display data when available', () => {

        component.sales$ = of(mockData);
        component.salesError$ = of(null);

        fixture.detectChanges();

        const tableElement = fixture.debugElement.query(By.css('.sales-data'));
        const loadingElement = fixture.debugElement.query(By.css('.sales-loading'));
        const errorElement = fixture.debugElement.query(By.css('.sales-error'));

        expect(tableElement).toBeTruthy();
        expect(loadingElement).toBeFalsy();
        expect(errorElement).toBeFalsy();
    });

    it('should display loading template while data is not available', () => {
        component.sales$ = of(null);
        component.salesError$ = of(null);

        fixture.detectChanges();

        const tableElement = fixture.debugElement.query(By.css('.sales-data'));
        const loadingElement = fixture.debugElement.query(By.css('.sales-loading'));
        const errorElement = fixture.debugElement.query(By.css('.sales-error'));

        expect(tableElement).toBeFalsy();
        expect(loadingElement).toBeTruthy();
        expect(errorElement).toBeFalsy();
    });

    it('should display error template when an error occurs', fakeAsync (() => {
        // Simulate an error in the observable

        component.sales$ = of(null);
        component.salesError$ = of("error");
        
        fixture.detectChanges();

        const tableElement = fixture.debugElement.query(By.css('.sales-data'));
        const loadingElement = fixture.debugElement.query(By.css('.sales-loading'));
        const errorElement = fixture.debugElement.query(By.css('.sales-error'));

        expect(tableElement).toBeFalsy();
        expect(loadingElement).toBeFalsy();
        expect(errorElement).toBeTruthy();
    }));
})


