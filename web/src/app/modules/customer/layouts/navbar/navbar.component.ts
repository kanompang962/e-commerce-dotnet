import { Component, OnInit } from '@angular/core';
import { navlist, sociallist } from '../../../../../assets/data/data';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{
  
  navlist: any;
  sociallist: any;
  carouselItems = [
    "https://img.freepik.com/free-photo/easter-celebration-with-dreamy-bunny_23-2151246597.jpg?size=626&ext=jpg",
    "https://img.freepik.com/free-photo/authentic-scene-young-person-undergoing-psychological-therapy_23-2150161977.jpg?t=st=1710134965~exp=1710135565~hmac=d950529a26b9d973ceb5facadd0b4fccfb63b1c3fcaf0770e5d8dab57264bdf2",
    "https://img.freepik.com/free-photo/beautiful-lesbian-couple-celebrating-their-wedding-day-outdoors_23-2150637608.jpg?t=st=1710134965~exp=1710135565~hmac=5c05921f23693f11c1be40e49b009f4c913baa66623b58b41821a13641e2ea0d"
  ];

  ngOnInit(): void {
    this.navlist = navlist
    this.sociallist = sociallist
  }
  
}
