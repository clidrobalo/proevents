import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {
  @Input() title?: string
  @Input() subTitle?: string
  @Input() iconClass?: string
  @Input() showBtnList: boolean = false;

  constructor(private route: Router) { }

  ngOnInit() {
  }

  list(): void {
    this.route.navigate([`/${this.title?.toLowerCase()}/list`]);
  }

}
