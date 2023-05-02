import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-min-length',
  templateUrl: './min-length.component.html',
  styleUrls: ['./min-length.component.css']
})
export class MinLengthComponent {
  @Input() minLength : number = 1;
}
