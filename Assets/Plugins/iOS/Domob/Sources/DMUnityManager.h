#ifndef Unity_iPhone_DomobManager_h
#define Unity_iPhone_DomobManager_h

#import "DMAdView.h"
#import "DMInterstitialAdController.h"

@interface DMSDKCallBackProxy : NSObject<DMAdViewDelegate,DMInterstitialAdControllerDelegate>
{
@private
    NSString* _unityObjectName;
}

- (id) initWithUnityObjectName:(NSString*) unityObjectName;

@end

#endif