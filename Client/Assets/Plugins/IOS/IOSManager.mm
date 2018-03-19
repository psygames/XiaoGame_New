
#include "IOSManager.h"
#import "SSKeychain.h"
#import "OpenUDID.h"

#include <sys/socket.h>
#include <sys/sysctl.h>
#include <net/if.h>
#include <net/if_dl.h>
#include <sys/utsname.h>

#define kUUID @"UUID"

void getUUID(){
    NSString * bunderIdentifier = [[NSBundle mainBundle] bundleIdentifier];
    NSLog(@"Entering GetAccountNumber(), bunderIdentifier:%@", bunderIdentifier);
    
    //[SSKeychain deletePasswordForService:bunderIdentifier account:kUUID];
    NSString *uuid = [SSKeychain passwordForService:bunderIdentifier account:kUUID];
    
    if (!uuid)
    {
        uuid = [OpenUDID value];
        //NSLog(@"GetAccountNumber(), get uuid:%@", uuid);
        if (!uuid || [uuid isEqualToString:[NSString stringWithFormat:@"%040x",0]])
        {
            //NSLog(@"GetAccountNumber(), get OpenUDID failed!");
            CFUUIDRef uuidRef = CFUUIDCreate(kCFAllocatorDefault);
            uuid = (NSString *)CFUUIDCreateString (kCFAllocatorDefault,uuidRef);
        }
        [SSKeychain setPassword:uuid forService:bunderIdentifier account:kUUID];
        //NSLog(@"GetAccountNumber(), Create new uuid:%@", uuid);
    }
    else
    {
        //NSLog(@"GetAccountNumber(), Already have uuid:%@", uuid);
    }
    
    UnitySendMessage("System", "ReciveUUID", uuid.UTF8String);
}

void getMacAddress(){
    printf("getMacAddress");
    static char szBuf[64];
    int             mib[6];
    size_t          len;
    char            *buf;
    unsigned char   *ptr;
    struct if_msghdr *ifm;
    struct sockaddr_dl *sdl;
    
    mib[0] = CTL_NET;
    mib[1] = AF_ROUTE;
    mib[2] = 0;
    mib[3] = AF_LINK;
    mib[4] = NET_RT_IFLIST;
    
    bool error = false;
    if((mib[5] = if_nametoindex("en0")) == 0)
    {
        printf("Error: if_nametoindex error/n");
        error = true;
    }
    
    if (!error && sysctl(mib, 6, NULL, &len, NULL, 0) < 0)
    {
        printf("Error: sysctl, take 1/n");
        error = true;
    }
    
    if (!error && (buf = (char*)malloc(len)) == NULL)
    {
        printf("Could not allocate memory. error!/n");
        error = true;
    }
    
    if (!error && sysctl(mib, 6, buf, &len, NULL, 0) < 0)
    {
        printf("Error: sysctl, take 2");
        error = true;
    }
    
    if (error)
    {
        UnitySendMessage("System", "ReciveMacAddress", "");
        return ;
    }
    
    ifm = (struct if_msghdr *)buf;
    sdl = (struct sockaddr_dl *)(ifm + 1);
    ptr = (unsigned char *)LLADDR(sdl);
    sprintf(szBuf, "%02X:%02X:%02X:%02X:%02X:%02X", *ptr, *(ptr+1), *(ptr+2), *(ptr+3), *(ptr+4), *(ptr+5));
    free(buf);
 
    NSString *string_content = [[NSString alloc] initWithCString:(const char*)szBuf
                                                        encoding:NSUTF8StringEncoding];
    
    UnitySendMessage("System", "ReciveMacAddress", string_content.UTF8String);
}