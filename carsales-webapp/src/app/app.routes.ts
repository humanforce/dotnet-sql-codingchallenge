import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'sales',
        loadChildren: () => import('./pages/sales-page/sales.page.module').then(m => m.SalesPageModule)
    },
    {
        path: '',
        redirectTo: 'sales',
        pathMatch: 'full'
    }
];
