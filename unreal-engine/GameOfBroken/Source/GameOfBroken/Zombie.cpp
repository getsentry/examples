// Fill out your copyright notice in the Description page of Project Settings.


#include "Zombie.h"

#include "Components/CapsuleComponent.h"

// Sets default values
AZombie::AZombie()
{
 	// Set this character to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;
	UE_LOG(LogActor, Warning, TEXT("Zombie ctor."));

}

// Called when the game starts or when spawned
void AZombie::BeginPlay()
{
	Super::BeginPlay();
	UE_LOG(LogActor, Warning, TEXT("Beginning to play.. Not for long."));
}

int counter = 0;

// Called every frame
void AZombie::Tick(float DeltaTime)
{
	if (counter > 600)
	{
		UE_LOG(LogActor, Error, TEXT("Ticking bomb exploded."));
		char* hi = 0;
		(*hi)++;
	}
	counter++;
	Super::Tick(DeltaTime);

}

// Called to bind functionality to input
void AZombie::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
	Super::SetupPlayerInputComponent(PlayerInputComponent);

}

