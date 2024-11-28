import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CreateComponent } from './create/create.component';
import { UpdateComponent } from './update/update.component';
import { DeleteComponent } from './delete/delete.component';

export const appRoutes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: 'create', component: CreateComponent },
    { path: 'update', component: UpdateComponent },
    { path: 'delete', component: DeleteComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    { path: '**', redirectTo: '/home' }
];
