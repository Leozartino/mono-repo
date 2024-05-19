import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HomeComponent } from './pages/home/home.component';
import { IncidentComponent } from './views/incident/incident.component';
import { IncidentCreateComponent } from './components/incident-create/incident-create.component';
import { IncidentReadComponent } from './components/incident-read/incident-read.component';
import { EstablishmentComponent } from './components/establishment/establishment.component';
import { IncidentUpdateComponent } from './components/incident-update/incident-update.component';
import { IncidentDeleteComponent } from './components/incident-delete/incident-delete.component';

export const routes: Routes = [
    {
        path:'',
        redirectTo : 'login',
        pathMatch: 'full'
    }, 
    {
        path: 'login',
        component: LoginComponent,
        
    },
    {
        path:'dashboard',
        component: DashboardComponent,
        children: [
            {
                path: 'home',
                component: HomeComponent
            },
            {
                path:'incident',
                component: IncidentCreateComponent
            },
            {
                path:'incident-read',
                component: IncidentReadComponent
            },
            {
                path:'establishment',
                component: EstablishmentComponent
            }
        ],
    },
    {
        path: 'incident/update/:id',
        component: IncidentUpdateComponent
    },
    {
        path: 'incident/delete/:id',
        component: IncidentDeleteComponent
    },
    // {
    //     path: 'incident/:id',
    //     component: IncidentComponent 	
    // }
];
