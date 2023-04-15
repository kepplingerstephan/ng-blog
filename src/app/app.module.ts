import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// tiny editor
import { EditorModule, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';
// bootstrap
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateBlogentryComponent } from './components/create-blogentry/create-blogentry.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UserComponent } from './components/user/user.component';

import {HttpClientModule} from '@angular/common/http';


@NgModule({
  declarations: [
    AppComponent,
    CreateBlogentryComponent,
    HomeComponent,
    NavbarComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    EditorModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    { provide: TINYMCE_SCRIPT_SRC, useValue: 'tinymce/tinymce.min.js' }, 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
