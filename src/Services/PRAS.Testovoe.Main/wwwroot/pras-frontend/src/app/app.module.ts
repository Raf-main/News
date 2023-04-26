import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AuthService} from "./services/auth/authentication.service";
import { LocalStorageService } from "./services/storages/local-storage.service";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { NewsListComponent } from './components/news-list/news-list.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { NewsComponent } from './components/news/news.component';
import { ErrorComponent } from './components/error/error.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { NewsService } from './services/news/news.service';
import { NewsCreateComponent } from './components/news-create/news-create.component';
import { HomeComponent } from './components/home/home.component';
import { NewsEditComponent } from './components/news-edit/news-edit.component';
import { LocaleSwitcherComponent } from './components/locale-switcher/locale-switcher.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    NewsCreateComponent,
    NewsListComponent,
    NewsEditComponent,
    NewsComponent,
    NotFoundComponent,
    ErrorComponent,
    LocaleSwitcherComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers: [
    AuthService,
    NewsService,
    LocalStorageService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
