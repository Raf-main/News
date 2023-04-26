import { environment } from './../../../../environments/environment';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs";
import { NewsResponse } from 'src/app/models/response/newsResponse';
import { NewsService } from 'src/app/services/news/news.service';

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit, OnDestroy{
  routeSub?: Subscription;
  news: NewsResponse | null = null;
  pictureUrl: string | null = null;

  constructor(private newsService: NewsService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.newsService.Get(params['id']).subscribe(news => {
        this.news = news;
        this.pictureUrl = `${environment.imageUrl}/${this.news.imageName}`;
      });
    });
  }

  ngOnDestroy() {
    this.routeSub?.unsubscribe();
  }
}
