#include <cstdint>

struct AccelCalibrationData
{
    uint32_t          CRC;

    int16_t           UseCalibration;
    int32_t           Scale;
    int32_t           XCalVector[4];
    int32_t           YCalVector[4];
    int32_t           ZCalVector[4];
    uint16_t         CalStatus;
};
