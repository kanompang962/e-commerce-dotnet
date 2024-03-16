import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {

  banner_start: any;

  ngOnInit(): void {
    this.banner_start = this.banners[0];
    this.banners = this.banners.slice(1);
  }
  @Input() banners: any[] = [];
}
