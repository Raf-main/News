import { environment } from 'environments/environment';
import { Component, OnInit } from '@angular/core';
import { NewsPreviewResponse } from 'src/app/models/response/newsPreviewResponse';
import { NewsService } from 'src/app/services/news/news.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  newsList: Array<NewsPreviewResponse> = [];
  imageApiUrl: string = environment.imageUrl;
  pageSize: number = 2;

  constructor(private newsService: NewsService) {
    this.newsList = [];
  }

  ngOnInit() {
    this.newsService.GetPaged(1, this.pageSize)
    .subscribe(
      newsResponseList => {
        this.newsList.push(...newsResponseList);
      }
    );
  }
}
