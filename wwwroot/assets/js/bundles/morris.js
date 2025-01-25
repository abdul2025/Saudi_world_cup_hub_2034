!function(){function p(t,i){return function(){return t.apply(i,arguments)}}function t(t,i){for(var e in i)o.call(i,e)&&(t[e]=i[e]);function s(){this.constructor=t}s.prototype=i.prototype,t.prototype=new s,t.__super__=i.prototype}var f,F,i,e,h=[].slice,o={}.hasOwnProperty,a=[].indexOf||function(t){for(var i=0,e=this.length;i<e;i++)if(i in this&&this[i]===t)return i;return-1};function s(){}function n(t){this.resizeHandler=p(this.resizeHandler,this);var n=this;if("string"==typeof t.element?this.el=f(document.getElementById(t.element)):this.el=f(t.element),null==this.el||0===this.el.length)throw new Error("Graph container element not found");"static"===this.el.css("position")&&this.el.css("position","relative"),this.options=f.extend({},this.gridDefaults,this.defaults||{},t),"string"==typeof this.options.units&&(this.options.postUnits=t.units),this.raphael=new Raphael(this.el[0]),this.elementWidth=null,this.elementHeight=null,this.dirty=!1,this.selectFrom=null,this.init&&this.init(),this.setData(this.options.data),this.el.bind("mousemove",function(t){var i,e,s=n.el.offset(),o=t.pageX-s.left;return n.selectFrom?(i=n.data[n.hitTest(Math.min(o,n.selectFrom))]._x,e=n.data[n.hitTest(Math.max(o,n.selectFrom))]._x,n.selectionRect.attr({x:i,width:e-i})):n.fire("hovermove",o,t.pageY-s.top)}),this.el.bind("mouseleave",function(t){return n.selectFrom&&(n.selectionRect.hide(),n.selectFrom=null),n.fire("hoverout")}),this.el.bind("touchstart touchmove touchend",function(t){var t=t.originalEvent.touches[0]||t.originalEvent.changedTouches[0],i=n.el.offset();return n.fire("hovermove",t.pageX-i.left,t.pageY-i.top)}),this.el.bind("click",function(t){var i=n.el.offset();return n.fire("gridclick",t.pageX-i.left,t.pageY-i.top)}),this.options.rangeSelect&&(this.selectionRect=this.raphael.rect(0,0,0,this.el.innerHeight()).attr({fill:this.options.rangeSelectColor,stroke:!1}).toBack().hide(),this.el.bind("mousedown",function(t){var i=n.el.offset();return n.startRange(t.pageX-i.left)}),this.el.bind("mouseup",function(t){var i=n.el.offset();return n.endRange(t.pageX-i.left),n.fire("hovermove",t.pageX-i.left,t.pageY-i.top)})),this.options.resize&&f(window).bind("resize",function(t){return null!=n.timeoutId&&window.clearTimeout(n.timeoutId),n.timeoutId=window.setTimeout(n.resizeHandler,100)}),this.el.css("-webkit-tap-highlight-color","rgba(0,0,0,0)"),this.postInit&&this.postInit()}function r(t){this.options=f.extend({},F.Hover.defaults,t=null==t?{}:t),this.el=f("<div class='"+this.options.class+"'></div>"),this.el.hide(),this.options.parent.append(this.el)}function l(t){if(this.hilight=p(this.hilight,this),this.onHoverOut=p(this.onHoverOut,this),this.onHoverMove=p(this.onHoverMove,this),this.onGridClick=p(this.onGridClick,this),!(this instanceof F.Line))return new F.Line(t);l.__super__.constructor.call(this,t)}function u(t){if(!(this instanceof F.Area))return new F.Area(t);t=f.extend({},i,t),this.cumulative=!t.behaveLikeLine,"auto"===t.fillOpacity&&(t.fillOpacity=t.behaveLikeLine?.8:1),u.__super__.constructor.call(this,t)}function c(t){if(this.onHoverOut=p(this.onHoverOut,this),this.onHoverMove=p(this.onHoverMove,this),this.onGridClick=p(this.onGridClick,this),!(this instanceof F.Bar))return new F.Bar(t);c.__super__.constructor.call(this,f.extend({},t,{parseTime:!1}))}function d(t){this.resizeHandler=p(this.resizeHandler,this),this.select=p(this.select,this),this.click=p(this.click,this);var i=this;if(!(this instanceof F.Donut))return new F.Donut(t);if(this.options=f.extend({},this.defaults,t),"string"==typeof t.element?this.el=f(document.getElementById(t.element)):this.el=f(t.element),null===this.el||0===this.el.length)throw new Error("Graph placeholder not found.");void 0!==t.data&&0!==t.data.length&&(this.raphael=new Raphael(this.el[0]),this.options.resize&&f(window).bind("resize",function(t){return null!=i.timeoutId&&window.clearTimeout(i.timeoutId),i.timeoutId=window.setTimeout(i.resizeHandler,100)}),this.setData(t.data))}function g(t,i,e,s,o,n,r,h,a,l){this.cx=t,this.cy=i,this.inner=e,this.outer=s,this.color=r,this.backgroundColor=h,this.index=a,this.raphael=l,this.deselect=p(this.deselect,this),this.select=p(this.select,this),this.sin_p0=Math.sin(o),this.cos_p0=Math.cos(o),this.sin_p1=Math.sin(n),this.cos_p1=Math.cos(n),this.is_long=n-o>Math.PI?1:0,this.path=this.calcSegment(this.inner+3,this.inner+this.outer-5),this.selectedPath=this.calcSegment(this.inner+3,this.inner+this.outer),this.hilight=this.calcArc(this.inner)}F=window.Morris={},f=jQuery,F.EventEmitter=(s.prototype.on=function(t,i){return null==this.handlers&&(this.handlers={}),null==this.handlers[t]&&(this.handlers[t]=[]),this.handlers[t].push(i),this},s.prototype.fire=function(){var t,i,e,s,o,n=arguments[0],r=2<=arguments.length?h.call(arguments,1):[];if(null!=this.handlers&&null!=this.handlers[n]){for(o=[],i=0,e=(s=this.handlers[n]).length;i<e;i++)t=s[i],o.push(t.apply(null,r));return o}},s),F.commas=function(t){var i,e;return null!=t?(e=t<0?"-":"",t=Math.abs(t),e+=(i=Math.floor(t).toFixed(0)).replace(/(?=(?:\d{3})+$)(?!^)/g,","),(t=t.toString()).length>i.length&&(e+=t.slice(i.length)),e):"-"},F.pad2=function(t){return(t<10?"0":"")+t},F.Grid=(e=F.EventEmitter,t(n,e),n.prototype.gridDefaults={dateFormat:null,axes:!0,grid:!0,gridLineColor:"#aaa",gridStrokeWidth:.5,gridTextColor:"#888",gridTextSize:12,gridTextFamily:"sans-serif",gridTextWeight:"normal",hideHover:!1,yLabelFormat:null,xLabelAngle:0,numLines:5,padding:25,parseTime:!0,postUnits:"",preUnits:"",ymax:"auto",ymin:"auto 0",goals:[],goalStrokeWidth:1,goalLineColors:["#666633","#999966","#cc6666","#663333"],events:[],eventStrokeWidth:1,eventLineColors:["#005a04","#ccffbb","#3a5f0b","#005502"],rangeSelect:null,rangeSelectColor:"#eef",resize:!1},n.prototype.setData=function(s,t){var o,n,r,i,h,a,l,p,u,c,d,f,e;if(null==t&&(t=!0),null!=(this.options.data=s)&&0!==s.length)return c=this.cumulative?0:null,d=this.cumulative?0:null,0<this.options.goals.length&&(e=Math.min.apply(Math,this.options.goals),i=Math.max.apply(Math,this.options.goals),d=null!=d?Math.min(d,e):e,c=null!=c?Math.max(c,i):i),this.data=function(){var t,i,e=[];for(r=t=0,i=s.length;t<i;r=++t)a=s[r],(h={src:a}).label=a[this.options.xkey],this.options.parseTime?(h.x=F.parseDate(h.label),this.options.dateFormat?h.label=this.options.dateFormat(h.x):"number"==typeof h.label&&(h.label=new Date(h.label).toString())):(h.x=r,this.options.xLabelFormat&&(h.label=this.options.xLabelFormat(h))),p=0,h.y=function(){var t,i,e=this.options.ykeys,s=[];for(n=t=0,i=e.length;t<i;n=++t)f=e[n],null!=(f=null!=(f="string"==typeof(f=a[f])?parseFloat(f):f)&&"number"!=typeof f?null:f)&&(this.cumulative?p+=f:null!=c?(c=Math.max(f,c),d=Math.min(f,d)):c=d=f),this.cumulative&&null!=p&&(c=Math.max(p,c),d=Math.min(p,d)),s.push(f);return s}.call(this),e.push(h);return e}.call(this),this.options.parseTime&&(this.data=this.data.sort(function(t,i){return(t.x>i.x)-(i.x>t.x)})),this.xmin=this.data[0].x,this.xmax=this.data[this.data.length-1].x,this.events=[],0<this.options.events.length&&(this.options.parseTime?this.events=function(){for(var t=this.options.events,i=[],e=0,s=t.length;e<s;e++)o=t[e],i.push(F.parseDate(o));return i}.call(this):this.events=this.options.events,this.xmax=Math.max(this.xmax,Math.max.apply(Math,this.events)),this.xmin=Math.min(this.xmin,Math.min.apply(Math,this.events))),this.xmin===this.xmax&&(--this.xmin,this.xmax+=1),this.ymin=this.yboundary("min",d),this.ymax=this.yboundary("max",c),this.ymin===this.ymax&&(d&&--this.ymin,this.ymax+=1),!0!==(e=this.options.axes)&&"both"!==e&&"y"!==e&&!0!==this.options.grid||(this.options.ymax===this.gridDefaults.ymax&&this.options.ymin===this.gridDefaults.ymin?(this.grid=this.autoGridLines(this.ymin,this.ymax,this.options.numLines),this.ymin=Math.min(this.ymin,this.grid[0]),this.ymax=Math.max(this.ymax,this.grid[this.grid.length-1])):(l=(this.ymax-this.ymin)/(this.options.numLines-1),this.grid=function(){var t,i,e=[];for(u=t=this.ymin,i=this.ymax;0<l?t<=i:i<=t;u=t+=l)e.push(u);return e}.call(this))),this.dirty=!0,t?this.redraw():void 0;this.data=[],this.raphael.clear(),null!=this.hover&&this.hover.hide()},n.prototype.yboundary=function(t,i){var e,s=this.options["y"+t];return"string"==typeof s?"auto"===s.slice(0,4)?5<s.length?(e=parseInt(s.slice(5),10),null==i?e:Math[t](i,e)):null!=i?i:0:parseInt(s,10):s},n.prototype.autoGridLines=function(t,i,e){var s,o,n=Math.floor(Math.log(i-t)/Math.log(10)),n=Math.pow(10,n),r=Math.floor(t/n)*n,h=Math.ceil(i/n)*n,a=(h-r)/(e-1);return 1===n&&1<a&&Math.ceil(a)!==a&&(a=Math.ceil(a),h=r+a*(e-1)),r<0&&0<h&&(r=Math.floor(t/a)*a,h=Math.ceil(i/a)*a),(a<1?(s=Math.floor(Math.log(a)/Math.log(10)),function(){var t,i=[];for(o=t=r;0<a?t<=h:h<=t;o=t+=a)i.push(parseFloat(o.toFixed(1-s)));return i}):function(){var t,i=[];for(o=t=r;0<a?t<=h:h<=t;o=t+=a)i.push(o);return i})()},n.prototype._calc=function(){var o,s,t=this.el.width(),i=this.el.height();if((this.elementWidth!==t||this.elementHeight!==i||this.dirty)&&(this.elementWidth=t,this.elementHeight=i,this.dirty=!1,this.left=this.options.padding,this.right=this.elementWidth-this.options.padding,this.top=this.options.padding,this.bottom=this.elementHeight-this.options.padding,!0!==(t=this.options.axes)&&"both"!==t&&"y"!==t||(i=function(){for(var t=this.grid,i=[],e=0,s=t.length;e<s;e++)o=t[e],i.push(this.measureText(this.yAxisFormat(o)).width);return i}.call(this),this.left+=Math.max.apply(Math,i)),!0!==(t=this.options.axes)&&"both"!==t&&"x"!==t||(i=function(){var t,i,e=[];for(s=t=0,i=this.data.length;0<=i?t<i:i<t;s=0<=i?++t:--t)e.push(this.measureText(this.data[s].text,-this.options.xLabelAngle).height);return e}.call(this),this.bottom-=Math.max.apply(Math,i)),this.width=Math.max(1,this.right-this.left),this.height=Math.max(1,this.bottom-this.top),this.dx=this.width/(this.xmax-this.xmin),this.dy=this.height/(this.ymax-this.ymin),this.calc))return this.calc()},n.prototype.transY=function(t){return this.bottom-(t-this.ymin)*this.dy},n.prototype.transX=function(t){return 1===this.data.length?(this.left+this.right)/2:this.left+(t-this.xmin)*this.dx},n.prototype.redraw=function(){if(this.raphael.clear(),this._calc(),this.drawGrid(),this.drawGoals(),this.drawEvents(),this.draw)return this.draw()},n.prototype.measureText=function(t,i){return null==i&&(i=0),i=(t=this.raphael.text(100,100,t).attr("font-size",this.options.gridTextSize).attr("font-family",this.options.gridTextFamily).attr("font-weight",this.options.gridTextWeight).rotate(i)).getBBox(),t.remove(),i},n.prototype.yAxisFormat=function(t){return this.yLabelFormat(t)},n.prototype.yLabelFormat=function(t){return"function"==typeof this.options.yLabelFormat?this.options.yLabelFormat(t):""+this.options.preUnits+F.commas(t)+this.options.postUnits},n.prototype.drawGrid=function(){var t,i,e,s,o,n,r,h;if(!1!==this.options.grid||!0===(o=this.options.axes)||"both"===o||"y"===o){for(h=[],e=0,s=(n=this.grid).length;e<s;e++)t=n[e],i=this.transY(t),!0!==(r=this.options.axes)&&"both"!==r&&"y"!==r||this.drawYAxisLabel(this.left-this.options.padding/2,i,this.yAxisFormat(t)),this.options.grid?h.push(this.drawGridLine("M"+this.left+","+i+"H"+(this.left+this.width))):h.push(void 0);return h}},n.prototype.drawGoals=function(){for(var t,i,e,s=this.options.goals,o=[],n=e=0,r=s.length;e<r;n=++e)i=s[n],t=this.options.goalLineColors[n%this.options.goalLineColors.length],o.push(this.drawGoal(i,t));return o},n.prototype.drawEvents=function(){for(var t,i,e,s=this.events,o=[],n=e=0,r=s.length;e<r;n=++e)i=s[n],t=this.options.eventLineColors[n%this.options.eventLineColors.length],o.push(this.drawEvent(i,t));return o},n.prototype.drawGoal=function(t,i){return this.raphael.path("M"+this.left+","+this.transY(t)+"H"+this.right).attr("stroke",i).attr("stroke-width",this.options.goalStrokeWidth)},n.prototype.drawEvent=function(t,i){return this.raphael.path("M"+this.transX(t)+","+this.bottom+"V"+this.top).attr("stroke",i).attr("stroke-width",this.options.eventStrokeWidth)},n.prototype.drawYAxisLabel=function(t,i,e){return this.raphael.text(t,i,e).attr("font-size",this.options.gridTextSize).attr("font-family",this.options.gridTextFamily).attr("font-weight",this.options.gridTextWeight).attr("fill",this.options.gridTextColor).attr("text-anchor","end")},n.prototype.drawGridLine=function(t){return this.raphael.path(t).attr("stroke",this.options.gridLineColor).attr("stroke-width",this.options.gridStrokeWidth)},n.prototype.startRange=function(t){return this.hover.hide(),this.selectFrom=t,this.selectionRect.attr({x:t,width:0}).show()},n.prototype.endRange=function(t){var i;if(this.selectFrom)return i=Math.min(this.selectFrom,t),t=Math.max(this.selectFrom,t),this.options.rangeSelect.call(this.el,{start:this.data[this.hitTest(i)].x,end:this.data[this.hitTest(t)].x}),this.selectFrom=null},n.prototype.resizeHandler=function(){return this.timeoutId=null,this.raphael.setSize(this.el.width(),this.el.height()),this.redraw()},n),F.parseDate=function(t){var i,e,s,o,n,r,h;return"number"==typeof t?t:(r=t.match(/^(\d+) Q(\d)$/),h=t.match(/^(\d+)-(\d+)$/),i=t.match(/^(\d+)-(\d+)-(\d+)$/),s=t.match(/^(\d+) W(\d+)$/),o=t.match(/^(\d+)-(\d+)-(\d+)[ T](\d+):(\d+)(Z|([+-])(\d\d):?(\d\d))?$/),n=t.match(/^(\d+)-(\d+)-(\d+)[ T](\d+):(\d+):(\d+(\.\d+)?)(Z|([+-])(\d\d):?(\d\d))?$/),r?new Date(parseInt(r[1],10),3*parseInt(r[2],10)-1,1).getTime():h?new Date(parseInt(h[1],10),parseInt(h[2],10)-1,1).getTime():i?new Date(parseInt(i[1],10),parseInt(i[2],10)-1,parseInt(i[3],10)).getTime():s?(4!==(r=new Date(parseInt(s[1],10),0,1)).getDay()&&r.setMonth(0,1+(4-r.getDay()+7)%7),r.getTime()+6048e5*parseInt(s[2],10)):o?o[6]?(e=0,"Z"!==o[6]&&(e=60*parseInt(o[8],10)+parseInt(o[9],10),"+"===o[7])&&(e=0-e),Date.UTC(parseInt(o[1],10),parseInt(o[2],10)-1,parseInt(o[3],10),parseInt(o[4],10),parseInt(o[5],10)+e)):new Date(parseInt(o[1],10),parseInt(o[2],10)-1,parseInt(o[3],10),parseInt(o[4],10),parseInt(o[5],10)).getTime():n?(h=parseFloat(n[6]),i=Math.floor(h),r=Math.round(1e3*(h-i)),n[8]?(e=0,"Z"!==n[8]&&(e=60*parseInt(n[10],10)+parseInt(n[11],10),"+"===n[9])&&(e=0-e),Date.UTC(parseInt(n[1],10),parseInt(n[2],10)-1,parseInt(n[3],10),parseInt(n[4],10),parseInt(n[5],10)+e,i,r)):new Date(parseInt(n[1],10),parseInt(n[2],10)-1,parseInt(n[3],10),parseInt(n[4],10),parseInt(n[5],10),i,r).getTime()):new Date(parseInt(t,10),0,1).getTime())},F.Hover=(r.defaults={class:"morris-hover morris-default-style"},r.prototype.update=function(t,i,e){return t?(this.html(t),this.show(),this.moveTo(i,e)):this.hide()},r.prototype.html=function(t){return this.el.html(t)},r.prototype.moveTo=function(t,i){var e,s=this.options.parent.innerWidth(),o=this.options.parent.innerHeight(),n=this.el.outerWidth(),r=this.el.outerHeight(),t=Math.min(Math.max(0,t-n/2),s-n);return(null==i||(e=i-r-10)<0&&o<(e=i+10)+r)&&(e=o/2-r/2),this.el.css({left:t+"px",top:parseInt(e)+"px"})},r.prototype.show=function(){return this.el.show()},r.prototype.hide=function(){return this.el.hide()},r),F.Line=(e=F.Grid,t(l,e),l.prototype.init=function(){if("always"!==this.options.hideHover)return this.hover=new F.Hover({parent:this.el}),this.on("hovermove",this.onHoverMove),this.on("hoverout",this.onHoverOut),this.on("gridclick",this.onGridClick)},l.prototype.defaults={lineWidth:3,pointSize:4,lineColors:["#0b62a4","#7A92A3","#4da74d","#afd8f8","#edc240","#cb4b4b","#9440ed"],pointStrokeWidths:[1],pointStrokeColors:["#ffffff"],pointFillColors:[],smooth:!0,xLabels:"auto",xLabelFormat:null,xLabelMargin:24,hideHover:!1},l.prototype.calc=function(){return this.calcPoints(),this.generatePaths()},l.prototype.calcPoints=function(){for(var o,n,t=this.data,i=[],e=0,s=t.length;e<s;e++)(o=t[e])._x=this.transX(o.x),o._y=function(){for(var t=o.y,i=[],e=0,s=t.length;e<s;e++)n=t[e],i.push(null!=n?this.transY(n):n);return i}.call(this),i.push(o._ymax=Math.min.apply(Math,[this.bottom].concat(function(){for(var t=o._y,i=[],e=0,s=t.length;e<s;e++)null!=(n=t[e])&&i.push(n);return i}())));return i},l.prototype.hitTest=function(t){var i,e,s,o;if(0===this.data.length)return null;for(i=e=0,s=(o=this.data.slice(1)).length;e<s&&!(t<(o[i]._x+this.data[i]._x)/2);i=++e);return i},l.prototype.onGridClick=function(t,i){var e=this.hitTest(t);return this.fire("click",e,this.data[e].src,t,i)},l.prototype.onHoverMove=function(t,i){t=this.hitTest(t);return this.displayHoverForRow(t)},l.prototype.onHoverOut=function(){if(!1!==this.options.hideHover)return this.displayHoverForRow(null)},l.prototype.displayHoverForRow=function(t){var i;return null!=t?((i=this.hover).update.apply(i,this.hoverContentForRow(t)),this.hilight(t)):(this.hover.hide(),this.hilight())},l.prototype.hoverContentForRow=function(t){for(var i,e,s=this.data[t],o="<div class='morris-hover-row-label'>"+s.label+"</div>",n=s.y,r=e=0,h=n.length;e<h;r=++e)i=n[r],o+="<div class='morris-hover-point' style='color: "+this.colorFor(s,r,"label")+"'>\n  "+this.options.labels[r]+":\n  "+this.yLabelFormat(i)+"\n</div>";return[o="function"==typeof this.options.hoverCallback?this.options.hoverCallback(t,this.options,o,s.src):o,s._x,s._ymax]},l.prototype.generatePaths=function(){var o,n,r,h;return this.paths=function(){var t,i,e,s=[];for(n=t=0,i=this.options.ykeys.length;0<=i?t<i:i<t;n=0<=i?++t:--t)h="boolean"==typeof this.options.smooth?this.options.smooth:(e=this.options.ykeys[n],0<=a.call(this.options.smooth,e)),1<(o=function(){for(var t=this.data,i=[],e=0,s=t.length;e<s;e++)void 0!==(r=t[e])._y[n]&&i.push({x:r._x,y:r._y[n]});return i}.call(this)).length?s.push(F.Line.createPath(o,h,this.bottom)):s.push(null);return s}.call(this)},l.prototype.draw=function(){var t;if(!0!==(t=this.options.axes)&&"both"!==t&&"x"!==t||this.drawXAxis(),this.drawSeries(),!1===this.options.hideHover)return this.displayHoverForRow(this.data.length-1)},l.prototype.drawXAxis=function(){var t,o,i,e,s,n=this,r=this.bottom+this.options.padding/2,h=null,a=null,l=function(t,i){var e,i=n.drawXAxisLabel(n.transX(i),r,t),t=i.getBBox();return i.transform("r"+-n.options.xLabelAngle),e=i.getBBox(),i.transform("t0,"+e.height/2+"..."),0!==n.options.xLabelAngle&&(t=-.5*t.width*Math.cos(n.options.xLabelAngle*Math.PI/180),i.transform("t"+t+",0...")),e=i.getBBox(),(null==h||h>=e.x+e.width||null!=a&&a>=e.x)&&0<=e.x&&e.x+e.width<n.el.width()?(0!==n.options.xLabelAngle&&(t=1.25*n.options.gridTextSize/Math.sin(n.options.xLabelAngle*Math.PI/180),a=e.x-t),h=e.x-n.options.xLabelMargin):i.remove()},p=this.options.parseTime?1===this.data.length&&"auto"===this.options.xLabels?[[this.data[0].label,this.data[0].x]]:F.labelSeries(this.xmin,this.xmax,this.width,this.options.xLabels,this.options.xLabelFormat):function(){for(var t=this.data,i=[],e=0,s=t.length;e<s;e++)o=t[e],i.push([o.label,o.x]);return i}.call(this);for(p.reverse(),s=[],i=0,e=p.length;i<e;i++)t=p[i],s.push(l(t[0],t[1]));return s},l.prototype.drawSeries=function(){var t,i,e,s,o,n;for(this.seriesPoints=[],t=i=s=this.options.ykeys.length-1;s<=0?i<=0:0<=i;t=s<=0?++i:--i)this._drawLineFor(t);for(n=[],t=e=o=this.options.ykeys.length-1;o<=0?e<=0:0<=e;t=o<=0?++e:--e)n.push(this._drawPointFor(t));return n},l.prototype._drawPointFor=function(t){var i,e,s,o,n,r;for(this.seriesPoints[t]=[],r=[],s=0,o=(n=this.data).length;s<o;s++)(i=null)!=(e=n[s])._y[t]&&(i=this.drawLinePoint(e._x,e._y[t],this.colorFor(e,t,"point"),t)),r.push(this.seriesPoints[t].push(i));return r},l.prototype._drawLineFor=function(t){var i=this.paths[t];if(null!==i)return this.drawLinePath(i,this.colorFor(null,t,"line"),t)},l.createPath=function(t,i,e){var s,o,n,r,h,a,l,p,u,c="";for(i&&(n=F.Line.gradients(t)),l={y:null},r=p=0,u=t.length;p<u;r=++p)null!=(s=t[r]).y&&(null!=l.y?i?(o=n[r],a=n[r-1],h=(s.x-l.x)/4,c+="C"+(l.x+h)+","+Math.min(e,l.y+h*a)+","+(s.x-h)+","+Math.min(e,s.y-h*o)+","+s.x+","+s.y):c+="L"+s.x+","+s.y:i&&null==n[r]||(c+="M"+s.x+","+s.y)),l=s;return c},l.gradients=function(t){for(var i,e,s,o,n=function(t,i){return(t.y-i.y)/(t.x-i.x)},r=[],h=o=0,a=t.length;o<a;h=++o)null!=(i=t[h]).y?(e=t[h+1]||{y:null},null!=(s=t[h-1]||{y:null}).y&&null!=e.y?r.push(n(s,e)):null!=s.y?r.push(n(s,i)):null!=e.y?r.push(n(i,e)):r.push(null)):r.push(null);return r},l.prototype.hilight=function(t){var i,e,s,o,n;if(null!==this.prevHilight&&this.prevHilight!==t)for(i=e=0,o=this.seriesPoints.length-1;0<=o?e<=o:o<=e;i=0<=o?++e:--e)this.seriesPoints[i][this.prevHilight]&&this.seriesPoints[i][this.prevHilight].animate(this.pointShrinkSeries(i));if(null!==t&&this.prevHilight!==t)for(i=s=0,n=this.seriesPoints.length-1;0<=n?s<=n:n<=s;i=0<=n?++s:--s)this.seriesPoints[i][t]&&this.seriesPoints[i][t].animate(this.pointGrowSeries(i));return this.prevHilight=t},l.prototype.colorFor=function(t,i,e){return"function"==typeof this.options.lineColors?this.options.lineColors.call(this,t,i,e):"point"===e&&this.options.pointFillColors[i%this.options.pointFillColors.length]||this.options.lineColors[i%this.options.lineColors.length]},l.prototype.drawXAxisLabel=function(t,i,e){return this.raphael.text(t,i,e).attr("font-size",this.options.gridTextSize).attr("font-family",this.options.gridTextFamily).attr("font-weight",this.options.gridTextWeight).attr("fill",this.options.gridTextColor)},l.prototype.drawLinePath=function(t,i,e){return this.raphael.path(t).attr("stroke",i).attr("stroke-width",this.lineWidthForSeries(e))},l.prototype.drawLinePoint=function(t,i,e,s){return this.raphael.circle(t,i,this.pointSizeForSeries(s)).attr("fill",e).attr("stroke-width",this.pointStrokeWidthForSeries(s)).attr("stroke",this.pointStrokeColorForSeries(s))},l.prototype.pointStrokeWidthForSeries=function(t){return this.options.pointStrokeWidths[t%this.options.pointStrokeWidths.length]},l.prototype.pointStrokeColorForSeries=function(t){return this.options.pointStrokeColors[t%this.options.pointStrokeColors.length]},l.prototype.lineWidthForSeries=function(t){return this.options.lineWidth instanceof Array?this.options.lineWidth[t%this.options.lineWidth.length]:this.options.lineWidth},l.prototype.pointSizeForSeries=function(t){return this.options.pointSize instanceof Array?this.options.pointSize[t%this.options.pointSize.length]:this.options.pointSize},l.prototype.pointGrowSeries=function(t){return Raphael.animation({r:this.pointSizeForSeries(t)+3},25,"linear")},l.prototype.pointShrinkSeries=function(t){return Raphael.animation({r:this.pointSizeForSeries(t)},25,"linear")},l),F.labelSeries=function(t,i,e,s,o){var n,r,h,a,l,p,u,c=200*(i-t)/e,e=new Date(t),d=F.LABEL_SPECS[s];if(void 0===d)for(l=0,p=(u=F.AUTO_LABEL_ORDER).length;l<p;l++)if(h=u[l],c>=(h=F.LABEL_SPECS[h]).span){d=h;break}for(void 0===d&&(d=F.LABEL_SPECS.second),n=(d=o?f.extend({},d,{fmt:o}):d).start(e),r=[];(a=n.getTime())<=i;)t<=a&&r.push([d.fmt(n),a]),d.incr(n);return r},F.LABEL_SPECS={decade:{span:1728e8,start:function(t){return new Date(t.getFullYear()-t.getFullYear()%10,0,1)},fmt:function(t){return""+t.getFullYear()},incr:function(t){return t.setFullYear(t.getFullYear()+10)}},year:{span:1728e7,start:function(t){return new Date(t.getFullYear(),0,1)},fmt:function(t){return""+t.getFullYear()},incr:function(t){return t.setFullYear(t.getFullYear()+1)}},month:{span:24192e5,start:function(t){return new Date(t.getFullYear(),t.getMonth(),1)},fmt:function(t){return t.getFullYear()+"-"+F.pad2(t.getMonth()+1)},incr:function(t){return t.setMonth(t.getMonth()+1)}},week:{span:6048e5,start:function(t){return new Date(t.getFullYear(),t.getMonth(),t.getDate())},fmt:function(t){return t.getFullYear()+"-"+F.pad2(t.getMonth()+1)+"-"+F.pad2(t.getDate())},incr:function(t){return t.setDate(t.getDate()+7)}},day:{span:864e5,start:function(t){return new Date(t.getFullYear(),t.getMonth(),t.getDate())},fmt:function(t){return t.getFullYear()+"-"+F.pad2(t.getMonth()+1)+"-"+F.pad2(t.getDate())},incr:function(t){return t.setDate(t.getDate()+1)}},hour:(e=function(i){return{span:60*i*1e3,start:function(t){return new Date(t.getFullYear(),t.getMonth(),t.getDate(),t.getHours())},fmt:function(t){return F.pad2(t.getHours())+":"+F.pad2(t.getMinutes())},incr:function(t){return t.setUTCMinutes(t.getUTCMinutes()+i)}}})(60),"30min":e(30),"15min":e(15),"10min":e(10),"5min":e(5),minute:e(1),"30sec":(e=function(i){return{span:1e3*i,start:function(t){return new Date(t.getFullYear(),t.getMonth(),t.getDate(),t.getHours(),t.getMinutes())},fmt:function(t){return F.pad2(t.getHours())+":"+F.pad2(t.getMinutes())+":"+F.pad2(t.getSeconds())},incr:function(t){return t.setUTCSeconds(t.getUTCSeconds()+i)}}})(30),"15sec":e(15),"10sec":e(10),"5sec":e(5),second:e(1)},F.AUTO_LABEL_ORDER=["decade","year","month","week","day","hour","30min","15min","10min","5min","minute","30sec","15sec","10sec","5sec","second"],F.Area=(e=F.Line,t(u,e),i={fillOpacity:"auto",behaveLikeLine:!1},u.prototype.calcPoints=function(){for(var o,n,r,t=this.data,i=[],e=0,s=t.length;e<s;e++)(o=t[e])._x=this.transX(o.x),n=0,o._y=function(){for(var t=o.y,i=[],e=0,s=t.length;e<s;e++)r=t[e],this.options.behaveLikeLine?i.push(this.transY(r)):(n+=r||0,i.push(this.transY(n)));return i}.call(this),i.push(o._ymax=Math.max.apply(Math,o._y));return i},u.prototype.drawSeries=function(){var t,i,e,s,o,n,r,h;for(this.seriesPoints=[],h=[],e=0,s=(i=(this.options.behaveLikeLine?function(){n=[];for(var t=0,i=this.options.ykeys.length-1;0<=i?t<=i:i<=t;0<=i?t++:t--)n.push(t);return n}:function(){r=[];for(var t=o=this.options.ykeys.length-1;o<=0?t<=0:0<=t;o<=0?t++:t--)r.push(t);return r}).apply(this)).length;e<s;e++)t=i[e],this._drawFillFor(t),this._drawLineFor(t),h.push(this._drawPointFor(t));return h},u.prototype._drawFillFor=function(t){var i=this.paths[t];if(null!==i)return i=i+("L"+this.transX(this.xmax)+","+this.bottom+"L"+this.transX(this.xmin)+","+this.bottom)+"Z",this.drawFilledPath(i,this.fillForSeries(t))},u.prototype.fillForSeries=function(t){t=Raphael.rgb2hsl(this.colorFor(this.data[t],t,"line"));return Raphael.hsl(t.h,this.options.behaveLikeLine?.9*t.s:.75*t.s,Math.min(.98,this.options.behaveLikeLine?1.2*t.l:1.25*t.l))},u.prototype.drawFilledPath=function(t,i){return this.raphael.path(t).attr("fill",i).attr("fill-opacity",this.options.fillOpacity).attr("stroke","none")},u),F.Bar=(e=F.Grid,t(c,e),c.prototype.init=function(){if(this.cumulative=this.options.stacked,"always"!==this.options.hideHover)return this.hover=new F.Hover({parent:this.el}),this.on("hovermove",this.onHoverMove),this.on("hoverout",this.onHoverOut),this.on("gridclick",this.onGridClick)},c.prototype.defaults={barSizeRatio:.75,barGap:3,barColors:["#0b62a4","#7a92a3","#4da74d","#afd8f8","#edc240","#cb4b4b","#9440ed"],barOpacity:1,barRadius:[0,0,0,0],xLabelMargin:50},c.prototype.calc=function(){var t;if(this.calcBars(),!1===this.options.hideHover)return(t=this.hover).update.apply(t,this.hoverContentForRow(this.data.length-1))},c.prototype.calcBars=function(){for(var o,n,t,i=this.data,e=[],s=t=0,r=i.length;t<r;s=++t)(o=i[s])._x=this.left+this.width*(s+.5)/this.data.length,e.push(o._y=function(){for(var t=o.y,i=[],e=0,s=t.length;e<s;e++)n=t[e],i.push(null!=n?this.transY(n):null);return i}.call(this));return e},c.prototype.draw=function(){var t;return!0!==(t=this.options.axes)&&"both"!==t&&"x"!==t||this.drawXAxis(),this.drawSeries()},c.prototype.drawXAxis=function(){for(var t,i,e,s,o=this.bottom+(this.options.xAxisLabelTopPadding||this.options.padding/2),n=null,r=null,h=[],a=s=0,l=this.data.length;0<=l?s<l:l<s;a=0<=l?++s:--s)i=this.data[this.data.length-1-a],e=(i=this.drawXAxisLabel(i._x,o,i.label)).getBBox(),i.transform("r"+-this.options.xLabelAngle),t=i.getBBox(),i.transform("t0,"+t.height/2+"..."),0!==this.options.xLabelAngle&&(e=-.5*e.width*Math.cos(this.options.xLabelAngle*Math.PI/180),i.transform("t"+e+",0...")),(null==n||n>=t.x+t.width||null!=r&&r>=t.x)&&0<=t.x&&t.x+t.width<this.el.width()?(0!==this.options.xLabelAngle&&(e=1.25*this.options.gridTextSize/Math.sin(this.options.xLabelAngle*Math.PI/180),r=t.x-e),h.push(n=t.x-this.options.xLabelMargin)):h.push(i.remove());return h},c.prototype.drawSeries=function(){var o,n,r,h,a,l,p,u,c,d=this.width/this.options.data.length,t=this.options.stacked?1:this.options.ykeys.length,f=(d*this.options.barSizeRatio-this.options.barGap*(t-1))/t;return this.options.barSize&&(f=Math.min(f,this.options.barSize)),t=d-f*t-this.options.barGap*(t-1),h=t/2,c=this.ymin<=0&&0<=this.ymax?this.transY(0):null,this.bars=function(){var t,i,e=this.data,s=[];for(o=t=0,i=e.length;t<i;o=++t)a=e[o],n=0,s.push(function(){var t,i,e=a._y,s=[];for(l=t=0,i=e.length;t<i;l=++t)null!==(u=e[l])?(u=c?(p=Math.min(u,c),Math.max(u,c)):(p=u,this.bottom),r=this.left+o*d+h,this.options.stacked||(r+=l*(f+this.options.barGap)),u=u-p,this.options.verticalGridCondition&&this.options.verticalGridCondition(a.x)&&this.drawBar(this.left+o*d,this.top,d,Math.abs(this.top-this.bottom),this.options.verticalGridColor,this.options.verticalGridOpacity,this.options.barRadius),this.options.stacked&&(p-=n),this.drawBar(r,p,f,u,this.colorFor(a,l,"bar"),this.options.barOpacity,this.options.barRadius),s.push(n+=u)):s.push(null);return s}.call(this));return s}.call(this)},c.prototype.colorFor=function(t,i,e){var s;return"function"==typeof this.options.barColors?(t={x:t.x,y:t.y[i],label:t.label},s={index:i,key:this.options.ykeys[i],label:this.options.labels[i]},this.options.barColors.call(this,t,s,e)):this.options.barColors[i%this.options.barColors.length]},c.prototype.hitTest=function(t){return 0===this.data.length?null:(t=Math.max(Math.min(t,this.right),this.left),Math.min(this.data.length-1,Math.floor((t-this.left)/(this.width/this.data.length))))},c.prototype.onGridClick=function(t,i){var e=this.hitTest(t);return this.fire("click",e,this.data[e].src,t,i)},c.prototype.onHoverMove=function(t,i){var e,t=this.hitTest(t);return(e=this.hover).update.apply(e,this.hoverContentForRow(t))},c.prototype.onHoverOut=function(){if(!1!==this.options.hideHover)return this.hover.hide()},c.prototype.hoverContentForRow=function(t){for(var i,e,s=this.data[t],o="<div class='morris-hover-row-label'>"+s.label+"</div>",n=s.y,r=e=0,h=n.length;e<h;r=++e)i=n[r],o+="<div class='morris-hover-point' style='color: "+this.colorFor(s,r,"label")+"'>\n  "+this.options.labels[r]+":\n  "+this.yLabelFormat(i)+"\n</div>";return[o="function"==typeof this.options.hoverCallback?this.options.hoverCallback(t,this.options,o,s.src):o,this.left+(t+.5)*this.width/this.data.length]},c.prototype.drawXAxisLabel=function(t,i,e){return this.raphael.text(t,i,e).attr("font-size",this.options.gridTextSize).attr("font-family",this.options.gridTextFamily).attr("font-weight",this.options.gridTextWeight).attr("fill",this.options.gridTextColor)},c.prototype.drawBar=function(t,i,e,s,o,n,r){var h=Math.max.apply(Math,r),h=0===h||s<h?this.raphael.rect(t,i,e,s):this.raphael.path(this.roundedRect(t,i,e,s,r));return h.attr("fill",o).attr("fill-opacity",n).attr("stroke","none")},c.prototype.roundedRect=function(t,i,e,s,o){return["M",t,(o=null==o?[0,0,0,0]:o)[0]+i,"Q",t,i,t+o[0],i,"L",t+e-o[1],i,"Q",t+e,i,t+e,i+o[1],"L",t+e,i+s-o[2],"Q",t+e,i+s,t+e-o[2],i+s,"L",t+o[3],i+s,"Q",t,i+s,t,i+s-o[3],"Z"]},c),F.Donut=(e=F.EventEmitter,t(d,e),d.prototype.defaults={colors:["#0B62A4","#3980B5","#679DC6","#95BBD7","#B0CCE1","#095791","#095085","#083E67","#052C48","#042135"],backgroundColor:"#FFFFFF",labelColor:"#000000",formatter:F.commas,resize:!1},d.prototype.redraw=function(){var t,i,e,s,o,n,r,h,a,l,p,u,c,d,f,g,m,y,v,x,w,b,M;for(this.raphael.clear(),i=this.el.width()/2,e=this.el.height()/2,c=(Math.min(i,e)-10)/3,d=p=0,m=(x=this.values).length;d<m;d++)p+=u=x[d];for(h=5/(2*c),t=1.9999*Math.PI-h*this.data.length,o=n=0,this.segments=[],s=f=0,y=(w=this.values).length;f<y;s=++f)u=w[s],(l=new F.DonutSegment(i,e,2*c,c,n,a=n+h+u/p*t,this.data[s].color||this.options.colors[o%this.options.colors.length],this.options.backgroundColor,o,this.raphael)).render(),this.segments.push(l),l.on("hover",this.select),l.on("click",this.click),n=a,o+=1;for(this.text1=this.drawEmptyDonutLabel(i,e-10,this.options.labelColor,15,800),this.text2=this.drawEmptyDonutLabel(i,10+e,this.options.labelColor,14),r=Math.max.apply(Math,this.values),M=[],g=o=0,v=(b=this.values).length;g<v;g++){if((u=b[g])===r){this.select(o);break}M.push(o+=1)}return M},d.prototype.setData=function(t){var o;return this.data=t,this.values=function(){for(var t=this.data,i=[],e=0,s=t.length;e<s;e++)o=t[e],i.push(parseFloat(o.value));return i}.call(this),this.redraw()},d.prototype.click=function(t){return this.fire("click",t,this.data[t])},d.prototype.select=function(t){for(var i=this.segments,e=0,s=i.length;e<s;e++)i[e].deselect();return this.segments[t].select(),t=this.data[t],this.setLabels(t.label,this.options.formatter(t.value,t))},d.prototype.setLabels=function(t,i){var e=2*(Math.min(this.el.width()/2,this.el.height()/2)-10)/3,s=1.8*e,o=e/2,e=e/3;return this.text1.attr({text:t,transform:""}),t=this.text1.getBBox(),o=Math.min(s/t.width,o/t.height),this.text1.attr({transform:"S"+o+","+o+","+(t.x+t.width/2)+","+(t.y+t.height)}),this.text2.attr({text:i,transform:""}),o=this.text2.getBBox(),t=Math.min(s/o.width,e/o.height),this.text2.attr({transform:"S"+t+","+t+","+(o.x+o.width/2)+","+o.y})},d.prototype.drawEmptyDonutLabel=function(t,i,e,s,o){t=this.raphael.text(t,i,"").attr("font-size",s).attr("fill",e);return null!=o&&t.attr("font-weight",o),t},d.prototype.resizeHandler=function(){return this.timeoutId=null,this.raphael.setSize(this.el.width(),this.el.height()),this.redraw()},d),F.DonutSegment=(e=F.EventEmitter,t(g,e),g.prototype.calcArcPoints=function(t){return[this.cx+t*this.sin_p0,this.cy+t*this.cos_p0,this.cx+t*this.sin_p1,this.cy+t*this.cos_p1]},g.prototype.calcSegment=function(t,i){var e=this.calcArcPoints(t),s=e[0],o=e[1],n=e[2],e=e[3],r=this.calcArcPoints(i),h=r[0],a=r[1],l=r[2],r=r[3];return"M"+s+","+o+"A"+t+","+t+",0,"+this.is_long+",0,"+n+","+e+"L"+l+","+r+"A"+i+","+i+",0,"+this.is_long+",1,"+h+","+a+"Z"},g.prototype.calcArc=function(t){var i=this.calcArcPoints(t),e=i[0],s=i[1],o=i[2],i=i[3];return"M"+e+","+s+"A"+t+","+t+",0,"+this.is_long+",0,"+o+","+i},g.prototype.render=function(){var t=this;return this.arc=this.drawDonutArc(this.hilight,this.color),this.seg=this.drawDonutSegment(this.path,this.color,this.backgroundColor,function(){return t.fire("hover",t.index)},function(){return t.fire("click",t.index)})},g.prototype.drawDonutArc=function(t,i){return this.raphael.path(t).attr({stroke:i,"stroke-width":2,opacity:0})},g.prototype.drawDonutSegment=function(t,i,e,s,o){return this.raphael.path(t).attr({fill:i,stroke:e,"stroke-width":3}).hover(s).click(o)},g.prototype.select=function(){if(!this.selected)return this.seg.animate({path:this.selectedPath},150,"<>"),this.arc.animate({opacity:1},150,"<>"),this.selected=!0},g.prototype.deselect=function(){if(this.selected)return this.seg.animate({path:this.path},150,"<>"),this.arc.animate({opacity:0},150,"<>"),this.selected=!1},g)}.call(this);