import { NgModule} from '@angular/core'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { SalesPageRoutingModule } from './sales.page.routing';
import { SalesPage } from './sales.page'


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        SalesPageRoutingModule,
    ],
    declarations: [
        SalesPage
    ]
})
export class SalesPageModule { }