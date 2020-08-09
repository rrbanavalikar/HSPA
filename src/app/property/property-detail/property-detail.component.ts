import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HousingService } from 'src/app/services/housing.service';
import { Property } from 'src/app/model/property';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-property-detail',
  templateUrl: './property-detail.component.html',
  styleUrls: ['./property-detail.component.css']
})
export class PropertyDetailComponent implements OnInit {
public propertyId: number;
property = new Property();
galleryOptions: NgxGalleryOptions[];
galleryImages: NgxGalleryImage[];


  constructor(private route: ActivatedRoute,
              private router: Router,
              private housingService: HousingService) { }

  ngOnInit() {
    this.propertyId = +this.route.snapshot.params['id'];
    this.route.data.subscribe(
      (data: Property) => {
        this.property = data['prp'];
      }
    );

    // this.route.params.subscribe(
    //   (params) => {
    //     this.propertyId = +params['id'];
    //     this.housingService.getProperty(this.propertyId).subscribe(
    //       (data: Property) => {
    //         this.property = data;
    //       },error => this.router.navigate(['/'])
    //     );
    //   }
    //);
    this.galleryOptions = [
      {
        width: '100%',
        height: '465px',
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide
      },
      // max-width 800
      {
        breakpoint: 800,
        width: '100%',
        height: '600px',
        imagePercent: 80,
        thumbnailsPercent: 20,
        thumbnailsMargin: 20,
        thumbnailMargin: 20
      },
      // max-width 400
      {
        breakpoint: 400,
        preview: false
      }
    ];

    this.galleryImages = [
      {
        small: 'assets/images/prop-1-interior.jpg',
        medium: 'assets/images/prop-1-interior.jpg',
        big: 'assets/images/prop-1-interior.jpg'
      },
      {
        small: 'assets/images/prop-2-interior.jpg',
        medium:'assets/images/prop-2-interior.jpg',
        big: 'assets/images/prop-2-interior.jpg'
      },
      {
        small: 'assets/images/prop-interior.jpg',
        medium: 'assets/images/prop-interior.jpg',
        big: 'assets/images/prop-interior.jpg'
      },{
        small: 'assets/images/prop-1.jpg',
        medium: 'assets/images/prop-1.jpg',
        big: 'assets/images/prop-1.jpg'
      },
      {
        small:'assets/images/prop-1.jpg',
        medium: 'assets/images/prop-1.jpg',
        big:'assets/images/prop-1.jpg'
      }
    ];

  }

}
