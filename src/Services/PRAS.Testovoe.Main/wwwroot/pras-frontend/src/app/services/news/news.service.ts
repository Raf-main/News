import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { NewsPreviewResponse } from 'src/app/models/response/newsPreviewResponse';
import { NewsResponse } from 'src/app/models/response/newsResponse';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  readonly newsUrl : string = environment.apiUrl + "/News";

  constructor(private http: HttpClient) {

  }

  public GetPaged(page: number, size: number) : Observable<NewsPreviewResponse[]> {
    let options = { params : { page : page, size: size } }
    return this.http.get<NewsPreviewResponse[]>(this.newsUrl + "/Paged", options);
  }

  public Get(id : number) : Observable<NewsResponse> {
    return this.http.get<NewsResponse>(`${this.newsUrl}/${id}`);
  }

  public Update(id: number, data: any) : Observable<any> {
    return this.http.put(`${this.newsUrl}/${id}`,data);
  }

  public UpdateImage(id: number, data: FormData) : Observable<any> {
    return this.http.put(`${this.newsUrl}/${id}/Image`, data);
  }

  public Delete(id : number) : Observable<any> {
    return this.http.delete(`${this.newsUrl}/${id}`);
  }

  public Create(data: any) : Observable<any> {
    return this.http.post(this.newsUrl, data);
  }
}
