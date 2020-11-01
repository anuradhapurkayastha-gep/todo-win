import { ChangeDetectionStrategy, Component, ViewEncapsulation,OnInit,OnDestroy,ChangeDetectorRef,TemplateRef,ComponentFactoryResolver, ViewContainerRef,ViewChild} from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  changeDetection:ChangeDetectionStrategy.OnPush,
})

export class AppComponent implements OnInit, OnDestroy {

  @ViewChild('selectContainer', { read: ViewContainerRef, static: true }) selectContainer: ViewContainerRef;
  @ViewChild("home",{static: true})  home:TemplateRef<any>;
  @ViewChild("trending",{static: true})  trending:TemplateRef<any>;
  @ViewChild("history",{static: true})  history:TemplateRef<any>;
   columnData = [{
                              'column1':"1",
                              'column2':"2",
                              'column3':"3",
                              'column4':"1",
                              'column5':"2",
                              'column7':"3"
                              },{
                                'column1':"11",
                                'column2':"21",
                                'column3':"31"
                                }];

  constructor(public cdRef: ChangeDetectorRef ) {}

  ngOnInit() { 
      this.loadPage();
  }

  onTabClick($event) {
     this.loadPage($event);
  }

  loadPage (selected =null) {
    if(!selected || (selected == 1)) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.home);
    }
    if (selected == 2) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.trending);
    }
    if (selected == 3) {
      this.selectContainer.length >0  && this.selectContainer.clear();
      this.selectContainer.createEmbeddedView(this.history);
    }
  }
 
  ngOnDestroy() {

  }
}
