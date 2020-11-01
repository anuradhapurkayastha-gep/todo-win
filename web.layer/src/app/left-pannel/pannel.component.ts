import { ChangeDetectionStrategy, Component, ViewEncapsulation, OnInit, OnDestroy, Renderer2, ElementRef, ViewChild, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'pannel',
  templateUrl: './pannel.component.html',
  styleUrls: ['./pannel.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush
})
export class LeftPannelComponent implements OnInit, OnDestroy {
 
  showMore = false;
  @ViewChild('showMoreFields', { read: ElementRef, static: false }) showMoreFields;
  @Output() clickEvent = new EventEmitter<any>();

  constructor(private renderer: Renderer2) {}

  ngOnInit() { }

  showMoreIcons() {
     this.showMore = !this.showMore;
     if(this.showMore) {
       this.renderer.removeClass(this.showMoreFields.nativeElement,'showMoreNone');
          this.renderer.addClass(this.showMoreFields.nativeElement,'showMoreVisible');
     } else {
       this.renderer.removeClass(this.showMoreFields.nativeElement,'showMoreVisible');
      this.renderer.addClass(this.showMoreFields.nativeElement,'showMoreNone');
     }
  }

  click(number) {
     this.clickEvent.emit(number);
  }


  ngOnDestroy() { }
}
