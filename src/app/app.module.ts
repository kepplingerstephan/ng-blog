import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
// tiny editor
import { EditorModule } from '@tinymce/tinymce-angular';
// bootstrap
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CreateBlogentryComponent } from './components/create-blogentry/create-blogentry.component';
import { HomeComponent } from './components/home/home.component';
import { NavbarComponent } from './components/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    CreateBlogentryComponent,
    HomeComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    EditorModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
