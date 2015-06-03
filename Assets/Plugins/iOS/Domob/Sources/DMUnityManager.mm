#import "DMUnityManager.h"

extern UIViewController* UnityGetGLViewController();
extern UIView* UnityGetGLView();
static NSMutableDictionary* sdkCallBackProxies = [[NSMutableDictionary alloc] initWithCapacity:10];
static NSMutableDictionary* domobViewDic = [[NSMutableDictionary alloc] initWithCapacity:10];

DMSDKCallBackProxy* GetSDKCallBackProxyByUnityObjectName(NSString* unityObjectName)
{
    @synchronized(sdkCallBackProxies)
    {
        DMSDKCallBackProxy* retVal = [sdkCallBackProxies objectForKey:unityObjectName];
        if (retVal == NULL)
        {
            retVal = [[DMSDKCallBackProxy alloc] initWithUnityObjectName:unityObjectName];
            [sdkCallBackProxies setObject:retVal forKey:unityObjectName];
            [retVal release];
        }
        return retVal;
    }
}

extern "C"
{
    void DomobAddBannerADViewWithAutoRefresh(float x,float y,int w,int h,const char* publisherid, const char* placementID,const char* delegateObject, const char* key,BOOL autoRefresh) {
        @autoreleasepool {
            if ([domobViewDic objectForKey:[NSString stringWithUTF8String:key]]) {
                NSLog(@"has an banner");
            }else{
                
                NSString* pubID = [NSString stringWithUTF8String:publisherid];
                NSString *placeID = nil;
                
                if (NULL != placementID) {
                    placeID = [NSString stringWithUTF8String:placementID];
                }
                if (((w != 320 || w != 0 ) && h != 50)&&(h != 80 && w != 488) &&(h != 90 && (w != 728 || w != 0))) {
                    NSLog(@"not support for your size please check it %f   %f",w,h);
                }else{
                    DMAdView *dmAdView = [[DMAdView alloc] initWithPublisherId:pubID
                                                                   placementId:placeID
                                                                   autorefresh:autoRefresh];
                    
                    if (w == 0) {
                         dmAdView.frame = CGRectMake(x, y,0,h);
                    }else{
                        [dmAdView setAdSize:CGSizeMake(w,h)];
                        dmAdView.frame = CGRectMake(x,y,w,h);
                    }
                   
                    
                    UIView *parentView = UnityGetGLView();
                    
                    [parentView addSubview:dmAdView];
                    
                    if (delegateObject != NULL && strlen(delegateObject) > 0) {
                        NSString* unityObjectName = [NSString stringWithUTF8String:delegateObject];
                        dmAdView.rootViewController = (UIViewController*)UnityGetGLViewController();
                        dmAdView.delegate = GetSDKCallBackProxyByUnityObjectName(unityObjectName);
                        
                    } else {
                        dmAdView.rootViewController = nil;
                    }
                    [dmAdView loadAd];
                    [domobViewDic setObject:[dmAdView retain] forKey:[NSString stringWithUTF8String:key]];
                    [dmAdView release];
                    
                }
            }
            
        }
    }
    void DomobAddBannerADView(float x,float y,float w,float h,const char* publisherid, const char* placementID, const char* delegateObject, const char* key) {
        @autoreleasepool {
            DomobAddBannerADViewWithAutoRefresh(x,y,w,h,publisherid,placementID,delegateObject,key,YES);
        }
    }
    
    
    void DomobInterstitialsAdInitialize (const char* publishID,const char* placementID,const char* delegateObject, const char* key) {
        @autoreleasepool {
            if ([domobViewDic objectForKey:[NSString stringWithUTF8String:key]]) {
                NSLog(@"has an Interstitial");
            }else{
                NSString* pubID = [NSString stringWithUTF8String:publishID];
                NSString *placeID = nil;
                
                if (NULL != placementID) {
                    placeID = [NSString stringWithUTF8String:placementID];
                }
                             
                NSString* unityObjectName = nil;
                if (delegateObject != NULL && strlen(delegateObject) > 0) {
                    unityObjectName = [NSString stringWithUTF8String:delegateObject];
                }
                
                DMInterstitialAdController *dmInterstitial = [[DMInterstitialAdController alloc] initWithPublisherId:pubID
                                                                                                         placementId:placeID
                                                                                                  rootViewController:(UIViewController*)UnityGetGLViewController()];
                
                dmInterstitial.delegate = GetSDKCallBackProxyByUnityObjectName(unityObjectName);
                [dmInterstitial loadAd];
                [domobViewDic setObject:[dmInterstitial retain] forKey:[NSString stringWithUTF8String:key]];
                [dmInterstitial release];
            }

        }
    }
    
    void DomobPresentInterstitialsAd(const char* key) {
        if ([domobViewDic objectForKey:[NSString stringWithUTF8String:key]]) {
            DMInterstitialAdController *owController = [domobViewDic objectForKey:[NSString stringWithUTF8String:key]];
            if (owController.isReady) {
                [owController present];
            }else{
                [owController loadAd];
            }
        }else{
            NSLog(@"no Interstitial ad");
        }
        
    }
    
    void DomobRemoveBannerADView(const char* key) {
        if ([domobViewDic objectForKey:[NSString stringWithUTF8String:key]]) {
            DMAdView *ad = [domobViewDic objectForKey:[NSString stringWithUTF8String:key]];
            ad.delegate = nil;
            [ad removeFromSuperview];
            [domobViewDic removeObjectForKey:[NSString stringWithUTF8String:key]]; //释放后请将字典的对应关键字参数也移除
        }else{
            NSLog(@"no banner");
        }
        
        
    }
    
    void DomobRemoveInterstitialsAd(const char* key) {
        if ([domobViewDic objectForKey:[NSString stringWithUTF8String:key]]) {
            DMInterstitialAdController *ad = [domobViewDic objectForKey:[NSString stringWithUTF8String:key]];
            ad.delegate = nil;
            [ad release];
            [domobViewDic removeObjectForKey:[NSString stringWithUTF8String:key]];
        }else{
            NSLog(@"no InterstitialAd");
        }
        
    }
}


@implementation DMSDKCallBackProxy

- (id) initWithUnityObjectName:(NSString*) unityObjectName
{
    self = [super init];
    if (self) {
        _unityObjectName = [unityObjectName retain];
    }
    return self;
}

- (void) dealloc
{
    [_unityObjectName release];
    [super dealloc];
}

#pragma mark DMAdView delegate

// 成功加载广告后，回调该方法
// This method will be used after load successfully
- (void)dmAdViewSuccessToLoadAd:(DMAdView *)adView
{
    NSLog(@"[Domob Sample] success to load ad.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMAdViewSuccessToLoadAd" UTF8String], "");
}

// 加载广告失败后，回调该方法
// This method will be used after load failed
- (void)dmAdViewFailToLoadAd:(DMAdView *)adView withError:(NSError *)error
{
    NSLog(@"[Domob Sample] fail to load ad. %@", error);
    NSString* msgSentToUnityObject = [NSString stringWithFormat:@"%i, %@", error.code, error.domain];
    UnitySendMessage([_unityObjectName UTF8String], [@"DMAdViewFailToLoadAd" UTF8String], [msgSentToUnityObject UTF8String]);
}

// 当将要呈现出 Modal View 时，回调该方法。如打开内置浏览器
// When will be showing a Modal View, this method will be called. Such as open built-in browser
- (void)dmWillPresentModalViewFromAd:(DMAdView *)adView
{
    NSLog(@"[Domob Sample] will present modal view.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMWillPresentModalViewFromAd" UTF8String], "");
}

// 当呈现的 Modal View 被关闭后，回调该方法。如内置浏览器被关闭。
// When presented Modal View is closed, this method will be called. Such as built-in browser is closed
- (void)dmDidDismissModalViewFromAd:(DMAdView *)adView
{
    NSLog(@"[Domob Sample] did dismiss modal view.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMDidDismissModalViewFromAd" UTF8String], "");
}
- (void)dmApplicationWillEnterBackgroundFromAd:(DMAdView *)adView
{
    NSLog(@"[Domob Sample] will enter background.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMApplicationWillEnterBackgroundFromAd" UTF8String], "");
}

#pragma mark DMInterstitialAdController Delegate
// 当插屏广告被成功加载后，回调该方法
// This method will be used after the ad has been loaded successfully
- (void)dmInterstitialSuccessToLoadAd:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] success to load ad.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialSuccessToLoadAd" UTF8String], "");
}

// 当插屏广告加载失败后，回调该方法
// This method will be used after failed
- (void)dmInterstitialFailToLoadAd:(DMInterstitialAdController *)dmInterstitial withError:(NSError *)err
{
    NSLog(@"[Domob Interstitial] fail to load ad. %@", err);
    NSString* msgSentToUnityObject = [NSString stringWithFormat:@"%i, %@", err.code, err.domain];
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialFailToLoadAd" UTF8String], [msgSentToUnityObject UTF8String]);
    
}

// 当插屏广告要被呈现出来前，回调该方法
// This method will be used before being presented
- (void)dmInterstitialWillPresentScreen:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] will present.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialWillPresentScreen" UTF8String], "");
}

// 当插屏广告被关闭后，回调该方法
// This method will be used after Interstitial view  has been closed
- (void)dmInterstitialDidDismissScreen:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] did dismiss.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialDidDismissScreen" UTF8String], "");
    
    // 插屏广告关闭后，加载一条新广告用于下次呈现
    //prepair for the next advertisement view
    [dmInterstitial loadAd];
}

// 当将要呈现出 Modal View 时，回调该方法。如打开内置浏览器。
// When will be showing a Modal View, call this method. Such as open built-in browser
- (void)dmInterstitialWillPresentModalView:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] will present modal view.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialWillPresentModalView" UTF8String], "");
}

// 当呈现的 Modal View 被关闭后，回调该方法。如内置浏览器被关闭。
// When presented Modal View is closed, this method will be called. Such as built-in browser is closed
- (void)dmInterstitialDidDismissModalView:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] did dismiss modal view.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialDidDismissModalView" UTF8String], "");
    
}
- (void)dmInterstitialApplicationWillEnterBackground:(DMInterstitialAdController *)dmInterstitial
{
    NSLog(@"[Domob Interstitial] will enter background.");
    UnitySendMessage([_unityObjectName UTF8String], [@"DMInterstitialApplicationWillEnterBackground" UTF8String], "");
}


@end
