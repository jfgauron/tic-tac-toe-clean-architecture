FROM ubuntu:24.04

ENV DEBIAN_FRONTEND=noninteractive
ENV SHELL /bin/bash

# Delete default user
RUN userdel -r ubuntu

# Install commons
RUN apt update && apt install -y git curl

# Install csharp
RUN apt install -y dotnet-sdk-8.0
RUN dotnet tool install --global dotnet-ef

# Setup default user
ARG USERNAME=admin
ARG USER_UID=1000
ARG USER_GID=$USER_UID

RUN groupadd --gid $USER_GID $USERNAME \
    && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME \
    # [Optional] Add sudo support. Omit if you don't need to install software after connecting.
    && apt-get update \
    && apt-get install -y sudo \
    && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME \
    && chmod 0440 /etc/sudoers.d/$USERNAME

USER $USERNAME
