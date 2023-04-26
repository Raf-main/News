import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from "./components/login/login.component";
import { RegisterComponent } from "./components/register/register.component";
import { NewsListComponent } from "./components/news-list/news-list.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { NewsComponent } from "./components/news/news.component";
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { NewsEditComponent } from './components/news-edit/news-edit.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, },
  { path: 'register', component: RegisterComponent },
  { path: 'news', component: NewsListComponent },
  { path: 'news/:id', component: NewsComponent },
  { path: 'news/:id/edit', component: NewsEditComponent },
  { path: 'error', component: ErrorComponent },
  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
