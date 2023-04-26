import { Component, Inject, LOCALE_ID } from '@angular/core';

@Component({
  selector: 'app-locale-switcher',
  templateUrl: './locale-switcher.component.html',
  styleUrls: ['./locale-switcher.component.css']
})
export class LocaleSwitcherComponent {

  locales = [
    { code: 'en-US', name: 'English' },
    { code: 'ru', name: 'Русский' },
  ];

  constructor(
    @Inject(LOCALE_ID) public activeLocale: string
  ) {}

  onChange() {
    window.location.href = `/${this.activeLocale}`;
  }

}
