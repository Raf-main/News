import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'environments/environment';
import { Subscription } from 'rxjs';
import { NewsResponse } from 'src/app/models/response/newsResponse';
import { NewsService } from 'src/app/services/news/news.service';

@Component({
  selector: 'app-news-edit',
  templateUrl: './news-edit.component.html',
  styleUrls: ['./news-edit.component.css']
})
export class NewsEditComponent implements OnInit {
  private routeSub?: Subscription;

  editForm: FormGroup;
  title: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(40)]);
  subtitle: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(200)]);
  text: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(9999)]);

  imageFile: any = null;


  initialNews: NewsResponse | null = null;
  initialImageUrl: string | null = null;

  formEnabled: boolean = false;
  isCorrectImageType: boolean = true;

  constructor(private fb: FormBuilder, private newsService: NewsService, private route: ActivatedRoute, public router: Router) {
    this.editForm = fb.group({
      'title': this.title,
      'subtitle': this.subtitle,
      'text': this.text,
    })
   }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.newsService.Get(params['id']).subscribe(news => {
        this.initialNews = news;
        this.initialImageUrl = `${environment.imageUrl}/${this.initialNews!.imageName}`;

        this.title.setValue(this.initialNews.title);
        this.subtitle.setValue(this.initialNews.subtitle);
        this.text.setValue(this.initialNews.text);

        this.formEnabled = true;
      });
    });
  }

  ngOnDestroy() {
    this.routeSub?.unsubscribe();
  }

  onImageChange($event: any) {
    if ($event.target.files && $event.target.files[0]) {
      let file = $event.target.files[0];

      if (file.type == "image/png") {
        this.formEnabled = false;
        this.imageFile = file;
        let formData = new FormData();
        formData.append("image",  this.imageFile);

        this.newsService.UpdateImage(this.initialNews!.id, formData).subscribe({
          next: (v) => {
            console.log(1);
            this.router.navigate([`news/${this.initialNews!.id}`]);
          },
          error: (e) => {
            console.log(e)
          },
        });

        return;
      }

      this.isCorrectImageType = false;
    }
  }

  onEditFormSubmit() {
    if (!this.formEnabled) {
      return;
    }

    if(this.editForm.touched && this.editForm.valid){
      this.formEnabled = false;
      var formData = new FormData();
      formData.append('title', this.editForm.get("title")?.value);
      formData.append('subtitle', this.editForm.get("subtitle")?.value);
      formData.append('text', this.editForm.get("text")?.value);

      this.newsService.Update(this.initialNews!.id ,formData).subscribe({
        next: (v) => {
          this.router.navigate([`/news/${this.initialNews!.id}`]);
        },
        error: (e) => {
          console.log(e)
        },
      });
    }
  }
}
