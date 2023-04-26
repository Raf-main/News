import { Component, OnDestroy, OnInit } from '@angular/core';
import { NewsPreviewResponse } from "../../models/response/newsPreviewResponse";
import { fromEvent, Subscription, tap, throttleTime } from "rxjs";
import { NewsService } from 'src/app/services/news/news.service';
import { AuthService } from 'src/app/services/auth/authentication.service';
import { environment } from 'environments/environment';

@Component({
  selector: 'app-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements OnInit, OnDestroy {
  public page: number = 0;
  public newsList: Array<NewsPreviewResponse> = [];
  public imageApiUrl : string = environment.imageUrl;
  private isEnd : boolean = false;
  private readonly pageSize: number = 30;
  private scrollEventSub: Subscription = Subscription.EMPTY;
  public readonly isAdmin: boolean = false;

  constructor(private authService: AuthService, private newsService: NewsService) {
    this.newsList = [];
    this.isAdmin = authService.isInRole("Admin");
  }

  ngOnInit(): void{
    this.GetPaged();

    this.scrollEventSub = fromEvent(window, 'scroll').pipe(
      throttleTime(300),
      tap(event => this.onScroll(event))
    ).subscribe();
  }

  ngOnDestroy(): void {
    this.scrollEventSub.unsubscribe();
  }

  public onScroll(event : any){
      if(this.isEnd){
        this.scrollEventSub.unsubscribe;

        return;
      }

      let documentRect = document.documentElement.getBoundingClientRect();

      if(documentRect.bottom < document.documentElement.clientHeight + 2000) {
        this.GetPaged();
      }
  }

  public GetPaged() {
    this.newsService.GetPaged(++this.page, this.pageSize)
    .subscribe(
      newsResponseList => {
        if (newsResponseList.length < this.pageSize) {
          this.isEnd = true;
        }

        this.newsList.push(...newsResponseList);
      }
    );
  }

  public Delete(id : number)
  {
    let isSure = confirm("Are you sure?");

    if (isSure && this.isAdmin){
      this.newsService.Delete(id).subscribe();

      for (let i = 0; i < this.newsList.length; i++) {
        if (this.newsList[i].id == id) {
          this.newsList.splice(i, 1);
          break;
        }
      }
    }
  }

  scrollToTop(): void {
    window.scroll(0, 0);
  }
}
