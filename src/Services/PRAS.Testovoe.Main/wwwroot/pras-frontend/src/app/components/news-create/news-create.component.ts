import { NewsService } from './../../services/news/news.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-news-create',
  templateUrl: './news-create.component.html',
  styleUrls: ['./news-create.component.css']
})
export class NewsCreateComponent implements OnInit {

  createForm: FormGroup;
  title: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(40)]);
  subtitle: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(200)]);
  text: FormControl = new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(9999)]);
  isCorrectImageType: boolean = false;
  isSubmitted: boolean = false;
  imageFile: any = null;

  constructor(private fb: FormBuilder, private newsService: NewsService) {
    this.createForm = fb.group({
      'title': this.title,
      'subtitle': this.subtitle,
      'text': this.text,
    })
   }

  ngOnInit() {
  }

  save() {
    if(this.isSubmitted){
      return;
    }

    this.isSubmitted = true;

    if(this.createForm.touched && this.createForm.valid){
      var formData = new FormData();
      formData.append('title', this.createForm.get("title")?.value);
      formData.append('subtitle', this.createForm.get("subtitle")?.value);
      formData.append('text', this.createForm.get("text")?.value);
      formData.append('image', this.imageFile);

      this.newsService.Create(formData).subscribe({
        next : (response) => {
          this.createForm.reset;
          this.isCorrectImageType = false;
          this.isSubmitted = false;
          this.imageFile = null;
         }
      });
    }
  }

  onImageChange($event: any) {
    if ($event.target.files && $event.target.files[0]) {
      let file = $event.target.files[0];

      if(file.type == "image/png") {
        this.isCorrectImageType = true;
        this.imageFile = file;

        return;
      }
    }

    this.isCorrectImageType = false;
  }
}
