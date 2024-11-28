import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { appRoutes } from './app.routes';

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule, // Add HttpClientModule here
        RouterModule.forRoot(appRoutes)
    ],
    providers: []
})
export class AppModule { }
