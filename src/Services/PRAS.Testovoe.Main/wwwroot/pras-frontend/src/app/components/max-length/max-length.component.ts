import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-max-length',
  templateUrl: './max-length.component.html',
  styleUrls: ['./max-length.component.css']
})
export class MaxLengthComponent {
  @Input() maxLength : number = 1;
}
