import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateBlogentryComponent } from './components/create-blogentry/create-blogentry.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  {
    path:'create',
    component: CreateBlogentryComponent
  },
  {
    path:'**',
    redirectTo:'/'
  },
  {
    path:'',
    component:HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
