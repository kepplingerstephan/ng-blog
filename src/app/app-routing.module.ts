import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateBlogentryComponent } from './components/create-blogentry/create-blogentry.component';
import { HomeComponent } from './components/home/home.component';
import { UserComponent } from './components/user/user.component';
import { DisplayBlogComponent } from './components/display-blog/display-blog.component';

const routes: Routes = [
  {
    path:
      'user',
    component: UserComponent
  },
  {
    path: 'create',
    component: CreateBlogentryComponent
  },
  {
    path: 'blog/:id',
    component: DisplayBlogComponent
  },
  {
    path: '**',
    redirectTo: '/'
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
